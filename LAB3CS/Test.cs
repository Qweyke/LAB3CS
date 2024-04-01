using System;
using System.IO;

namespace LAB3CS
{
    internal class Test
    {
        static void Main(string[] args)
        {
            BaseList<int> abstr = new ArrList<int>();
            BaseList<int> chain = new ChainList<int>();

            Random rnd = new Random();

            int iter = 2000;

            for (int i = 0; i < iter; i++)
            {
                int ops = rnd.Next(5);
                int value = rnd.Next(100);
                //double value = rnd.NextDouble() * 2000;
                //bool value = rnd.Next(2) == 0;
                int pos = rnd.Next(2000);

                switch (ops)
                {
                    case 0:

                        abstr.Add(value);
                        chain.Add(value);
                        break;

                    case 1:

                        abstr.Delete(pos);
                        chain.Delete(pos);
                        break;

                    case 2:

                        abstr.Insert(value, pos);
                        chain.Insert(value, pos);

                        break;

                    /*case 3:
                        //Console.WriteLine("clr");
                        abstr.Clear();
                        chain.Clear();
                        break;*/

                    case 4:

                        abstr[pos] = value;
                        chain[pos] = value;
                        break;
                }
            }

            abstr.Sort();
            chain.Sort();

            if (abstr.IsEqual(chain)) Console.WriteLine("Успешно для 1 теста");
            else Console.WriteLine("Шляпа для 1 теста");

            for (int i = 0; i < iter; i++)
            {
                int ops2 = rnd.Next(6);

                switch (ops2)
                {
                    case 0:
                        chain = abstr.Clone();
                        break;

                    case 1:
                        abstr = chain.Clone();
                        break;

                    case 2:
                        abstr.Assign(chain);
                        break;

                    case 3:
                        chain.Assign(abstr);
                        break;

                    case 4:
                        chain.AssignTo(abstr);
                        break;

                    case 5:
                        abstr.AssignTo(chain);
                        break;
                }
            }

            if (abstr.IsEqual(chain)) Console.WriteLine("Успешно для 2 теста");
            else Console.WriteLine("Шляпа");

            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine("Текущий рабочий каталог: " + currentDirectory);

            string path = "list.txt";
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(abstr.ToString());
                writer.WriteLine(chain.ToString());
            }            
    
            Console.WriteLine("\n\nНажмите любую клавишу");
            Console.ReadKey();
        }
    }
}