using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10
{
    public class LoopedList<T> : IEnumerable<T>
    {
        public LoopedList()
        {

        }

        public LoopedList(IEnumerable<T> list)
        {
            Build(list);
        }

        public LoopedList(params T[] elements)
        {
            Build(elements.AsEnumerable());
        }

        /*public LoopedList<T> MakeProgressive(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("Количество должно быть натуральным числом.");

            var list = new LoopedList<T>();

            while (count > 0)
            {
                InsertAt()

                --count;
            }
        }*/

        public void InsertAt(T element, int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("Индекс должен быть неотрицательным числом.");

            if (mHead == null)
            {
                mHead = new Node(element);
                mHead.Previous = mHead;
                mHead.Next = mHead;
            }
            else
            {
                index %= Count;

                Node cur = Node.Advance(mHead, index);

                Node temp = new Node(element, cur.Previous, cur);
                cur.Previous.Next = temp;
                cur.Previous = temp;
            }

            ++Count;
        }

        public int RemoveAt(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("Неверный индекс.");

            if (Count == 1)
            {
                mHead = null;
            }
            else
            {
                index %= Count;

                Node cur = Node.Advance(mHead, index);
                Node previous = cur.Previous;
                Node next = cur.Next;

                previous.Next = next;
                next.Previous = previous;

                if (cur == mHead)
                    mHead = next;
            }

            --Count;

            return index;
        }

        private void Build(IEnumerable<T> list)
        {
            foreach (var v in list)
            {
                InsertAt(v, 0);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (mHead != null)
            {
                yield return mHead.Data;

                Node cur = mHead;

                while ((cur = cur.Next) != mHead)
                    yield return cur.Data;
            }
            else
            {
                throw new LoopedListEmptyException();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var v in this)
            {
                sb.AppendFormat("{0} -> ", v);
            }
            if (sb.Length > 4)
                sb.Length -= 4;

            return sb.ToString();
        }

        private class Node
        {
            public Node()
            {

            }

            public Node(T data)
            {
                Data = data;
            }

            public Node(T data, Node previous, Node next)
            {
                Data = data;
                Previous = previous;
                Next = next;
            }

            public static Node Advance(Node cur, int amount)
            {
                while (amount-- > 0)
                {
                    cur = cur.Next;
                }

                return cur;
            }

            public override string ToString()
            {
                return Data.ToString();
            }

            public T Data { get; set; }
            public Node Previous { get; set; }
            public Node Next { get; set; }
        }

        public int Count { get; set; }

        private Node mHead = null;
    }
}
