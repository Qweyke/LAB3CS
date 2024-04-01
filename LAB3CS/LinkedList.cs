
using System;
using System.Text;

namespace LAB3CS
{
    public class ChainList<T> : BaseList<T> where T : IComparable<T>
    {
        public class Node
        {
            public T Data
            {
                set; get;
            }
            public Node Next
            {
                set; get;
            }
            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }

        Node head = null;

        public Node Find(int posit)
        {
            if (posit >= count || head == null) return null; //if no el rtrn nthng

            int i = 0;
            Node P = head;

            while (P != null && i < posit)
            {
                P = P.Next;
                i++;
            }

            if (i == posit) return P;
            else return null;
        }
        public override void Add(T value)
        {
            if (head == null)
            {
                head = new Node(value);
            }

            else
            {
                Node last = Find(count - 1);
                last.Next = new Node(value);

            }
            count++;
        }
        public override void Insert(T value, int posit)
        {
            if (posit == count && posit == 0) Add(value);

            else if (posit == count) Add(value);

            else if (posit < count)
            {
                if (posit == 0)
                {
                    //Node curr = Find(posit);
                    head = new Node(value) { Next = head };
                    count++;
                }

                else
                {
                    Node prev = Find(posit - 1);
                    Node curr = Find(posit);
                    Node insr = new Node(value) { Next = curr };
                    prev.Next = insr;
                    count++;
                }
            }
        }
        public override void Delete(int posit)
        {
            if (posit < count && posit > 0)
            {
                Node prev = Find(posit - 1);
                Node current = prev.Next;
                if (current.Next != null) prev.Next = current.Next;
                else prev.Next = null;
                count--;
            }

            else if (posit == 0 && posit < count)
            {
                head = head.Next;
                count--;
            }

            else if (posit == 0 && count == 1)
            {
                head = null;
                count--;
            }
        }
        public override void Clear()
        {
            head = null;
            count = 0;
        }

        public override T this[int i]
        {
            get
            {
                if (i >= count || i < 0) return default;

                Node shw = Find(i);
                return shw.Data;
            }

            set
            {
                if (i >= count || i < 0) return;

                Node st = Find(i);
                st.Data = value;
            }
        }

        public override void Show()
        {
            Node cur = head;
            if (cur != null)
            {
                while (cur.Next != null)
                {
                    Console.Write($"{cur.Data}; ");
                    cur = cur.Next;
                }
                Console.Write($"{cur.Data}. ");
            }
            else Console.WriteLine("Нет элементов в chain листе");
        }
        protected override BaseList<T> Dummy()
        {
            return new ChainList<T>();
        }

        public override void Sort()
        {
            if (count == 0 || count == 1) { return; }

            bool swap;
            do
            {
                swap = false;
                Node curr = head;
                for (int i = 0; i < count - 1; i++)
                {
                    if (curr.Data.CompareTo(curr.Next.Data) < 0)
                    {
                        (curr.Next.Data, curr.Data) = (curr.Data, curr.Next.Data);
                        swap = true;
                    }
                    curr = curr.Next;
                }
            } while (swap);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Node cur = head;
            if (cur != null)
            {
                while (cur.Next != null)
                {
                    sb.Append($"[{cur.Data}]; ");
                    cur = cur.Next;
                }
                sb.Append($"[{cur.Data}].\n ");
                return sb.ToString();
            }
            else
            {
                sb.Append("Нет элементов в chain листе");
                return sb.ToString();
            } 
        }
    }
}