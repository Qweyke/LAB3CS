using System;
using System.IO;

namespace LAB3CS
{
    internal class Test
    {
        static void Main(string[] args)
        {
            string path = "list.txt";

            BaseList<int> abstr = new ArrList<int>();
            BaseList<int> chain = new ChainList<int>();

            Random rnd = new Random();

            chain.Activated += chain.Handler;
            abstr.Activated += abstr.Handler;

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
            }// test 1
            
            if (abstr == chain) Console.WriteLine("Успешно для 1 теста");
            else Console.WriteLine("Шляпа для 1 теста");

            if (abstr.EventCount == chain.EventCount) Console.WriteLine($"Успешно. Событий в abstr {abstr.EventCount}, событий в chain {chain.EventCount}");
            else Console.WriteLine("Шляпа по событиям");

            if (abstr.ExCount == chain.ExCount) Console.WriteLine($"Успешно. Кол-во исключений {abstr.ExCount}, {chain.ExCount}");
            else Console.WriteLine("Шляпа по исключениям");

            abstr.Show();
            chain.Show();           
            BaseList<int> merged1 = chain + abstr;
            BaseList<int> merged2 = abstr + chain;
            merged1.Show();
            merged2.Show();

            /*for (int i = 0; i < iter; i++)
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
            }// test 2*/
            //if (abstr == chain) Console.WriteLine("Успешно для 2 теста");
            //else Console.WriteLine("Шляпа для 2 теста");

            //if (abstr.ExCount == chain.ExCount) 
            //else Console.WriteLine("Шляпа");


            //abstr.Sort();
            //chain.Sort();           

            //abstr.SaveToFile(path);
            //chain.SaceToFile(path);
            //abstr.LoadFromFile(path);
            chain.LoadFromFile(path);

            Console.WriteLine($"Кол-во  файловых искл для {chain.ExFileCount}");

            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine("\nТекущий рабочий каталог: " + currentDirectory);
                    
            Console.WriteLine("\n\nНажмите любую клавишу");
            Console.ReadKey();
        }
    }
}