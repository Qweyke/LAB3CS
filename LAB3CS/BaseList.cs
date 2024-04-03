using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace LAB3CS
{
    public abstract class BaseList<T>: IEnumerable<T> where T : IComparable<T>
    {
        protected int count = 0;
        protected int ex_count = 0;
        protected int ex_file_count = 0;
        protected int event_count = 0;
        public int Count { get { return count; } }
        public int ExCount { get { return ex_count; } }
        public int ExFileCount { get { return ex_file_count; } }
        public int EventCount { get { return event_count; } }
        protected abstract BaseList<T> Dummy();

        public delegate void MethodListener();
        public event MethodListener Activated;
        protected void OnListMethod()
        {
            Activated?.Invoke();
        }
        public void Handler()
        {
            event_count++;
        }

        public abstract void Add(T val);
        public abstract void Delete(int pos);
        public abstract void Insert(T val, int pos);
        public abstract void Clear();
        public abstract T this[int i] { set; get; }
        public abstract void Show();
        public void Assign(BaseList<T> source)
        {
            Clear();
            for (int i = 0; i < source.Count; i++) Add(source[i]);
        }
        public void AssignTo(BaseList<T> dest)
        {
            dest.Assign(this);
        }
        public BaseList<T> Clone()
        {
            BaseList<T> clone = Dummy();
            clone.Assign(this);
            return clone;
        }
        public virtual void Sort()
        {
            if (this.Count == 0 || this.Count == 1) { return; }

            int pstn = 0;
            while (pstn < this.Count - 1)
            {
                if (this[pstn].CompareTo(this[pstn + 1]) >= 0)
                {
                    pstn++;
                }

                else
                {
                    (this[pstn + 1], this[pstn]) = (this[pstn], this[pstn + 1]);

                    if (pstn > 0) pstn--;
                }
            }
        }
        public bool IsEqual(BaseList<T> list)
        {
            if (this.Count > 0)
            {
                for (int i = 0; i <= this.Count; i++)
                {
                    if (this[i].CompareTo(list[i]) != 0) return false;
                }
                return true;
            }
            else if (this.Count == 0) return true;
            else return false;
        }
        public class BadIndexException : Exception
        {
            public BadIndexException(string message) : base(message)
            {

            }
        }
        public class BadFileException : FormatException
        {
            public BadFileException(string message) : base(message)
            {

            }
        }
        public void SaveToFile(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(this.ToString());
            }
        }
        public void LoadFromFile(string path)
        {
            this.Clear();

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    try
                    {
                        string list = reader.ReadToEnd();

                        list = list.Replace("[", "").Replace("]", "").Replace(".", "").Replace("\n", "");
                        string[] elems = list.Split(',');
                        foreach (string el in elems)
                        {
                            string trimmed = el.Trim();
                            T conv_el = (T)Convert.ChangeType(trimmed, typeof(T));
                            this.Add(conv_el);
                        }
                    }                   
                    catch (FormatException)
                    {
                        throw new BadFileException("Неверный формат данных в файле");
                    }
                    finally 
                    {
                        //Console.Write("Данные копированные в лист - ");
                        //this.Show();
                    }

                }
            }
            catch (BadFileException)
            {
                ex_file_count++;
                this.Clear();
            }
        }
        public static BaseList<T> operator +(BaseList<T> left, BaseList<T> right) 
        {
            BaseList<T> merged = left.Clone();
            for (int i = 0; i < right.Count; i++) 
            {
                merged.Add(right[i]);
            }
            return merged;
        }
        public static bool operator ==(BaseList<T> left, BaseList<T> right)
        {
            if (left.IsEqual(right)) return true;
            else return false;
        }
        public static bool operator !=(BaseList<T> left, BaseList<T> right)
        {
            if (left.IsEqual(right)) return false;
            else return true;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnum(this);
        }
        protected class ListEnum : IEnumerator<T>
        {
            private readonly BaseList<T> list;
            private int index;

            public ListEnum(BaseList<T> list) 
            {           
                this.list = list;
                index = -1;
            }

            public T Current
            {
                get { return list[index]; }
            }

            object IEnumerator.Current { get { return Current; } }

            public bool MoveNext() 
            {
                if (index < list.Count - 1) 
                {
                    index++;
                    return  true;
                }
                else return false;
            }

            public void Reset() { index = -1; } 

            public void Dispose() { }
        }
        
    }
}
