using System;
using System.Text;

namespace LAB3CS
{
    public class ArrList<T> : BaseList<T> where T : IComparable<T>
    {
        T[] buf;
        int size = 1;
        public ArrList()
        {
            buf = new T[size];
        }

        private void Expd()
        {
            size *= 2;
            Array.Resize(ref buf, size);
        }

        public override void Add(T val)
        {
            if (count >= size) { Expd(); }
            buf[count] = val;
            count++;
            OnListMethod();

        }
        public override void Insert(T val, int pos)
        {
            if (pos == count && pos == 0) Add(val);

            else if (pos == count) Add(val);

            else if (pos < count)
            {
                count++;
                if (count >= size) Expd();

                for (int i = count - 1; i != pos; i--)
                {
                    buf[i] = buf[i - 1];
                }
                buf[pos] = val;
            }
            OnListMethod();
        }
        public override void Delete(int pos)
        {
            if (pos < count)
            {
                for (int i = pos; i < count - 1; i++)
                {
                    buf[i] = buf[i + 1];
                }
                count--;
            }

            else if (pos == count - 1 && count > 0)
            {
                buf[pos] = default;
                count--;
            }
            OnListMethod();
        }

        public override void Clear()
        {
            for (int i = 0; i < count; i++)
            {
                buf[i] = default;
            }
            count = 0;
        }

        
        public override T this[int i]
        {
            get
            {
                try
                {
                    if (i >= count) throw new BadIndexException("Позиция выходит за рамки листа");
                    if (i < 0) throw new BadIndexException("Позиция имеет отрицательное значение");
                    return buf[i];
                }
                catch(BadIndexException)
                {

                    ex_count++;
                    return default;
                }
                finally { OnListMethod();  }
            }

            set
            {
                try
                {
                    if (i >= count) throw new BadIndexException("Позиция выходит за рамки листа");
                    if (i < 0) throw new BadIndexException("Позиция имеет отрицательное значение");
                    buf[i] = value;
                }
                catch(BadIndexException)
                {
                    ex_count++;
                }
                finally { OnListMethod(); }
            }
        }

        public override void Show()
        {
            if (count >= 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (i == count - 1) Console.Write($"{buf[i]}. ");
                    else Console.Write($"{buf[i]}, ");
                }
            }
            else Console.WriteLine("Нет элементов в array листе");
            Console.WriteLine("\n\n");
        }
        protected override BaseList<T> Dummy()
        {
            return new ArrList<T>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (count >= 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (i == count - 1) sb.Append($"[{buf[i]}].\n ");
                    else sb.Append($"[{buf[i]}], ");
                }
                return sb.ToString();
            }
            else
            {
                sb.Append("Нет элементов в array листе");
                return sb.ToString(); 
            }           
        }
    }
}
