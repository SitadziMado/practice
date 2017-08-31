using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9
{
    public class IntList : ICollection<int>
    {
        private IntList()
        {
            mRoot = new NullNode() { Previous = new NullNode() };
            mRoot.Previous.Previous = mRoot.Previous;
            mEnd = mRoot;
        }

        public static IntList MakeProgressive(int count)
        {
            IntList result = new IntList();
            result.Build(count);
            return result;
        }

        public int? Find(int value)
        {
            int? index = 0;
            FindImpl(value, mRoot, ref index);
            return index;
        }

        public bool Remove(int value)
        {
            int? index = 0;
            Node toRemove = FindImpl(value, mRoot, ref index);

            // Если элемент найден
            //
            if (toRemove.Valid)
            {
                Node previous = toRemove.Previous;
                Node next = toRemove.Next;

                if (previous.Valid)
                    previous.Next = next;

                next.Previous = previous;

                if (toRemove == mRoot)
                    mRoot = next;

                --mCount;

                Remove(value);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Add(int item)
        {
            var previous = mEnd.Previous;

            var node = new Node(item, previous, mEnd);

            ++mCount;
        }

        public void Clear()
        {
            mEnd.Previous = mRoot.Previous;
            mRoot = mEnd;
        }

        public bool Contains(int item)
        {
            return Find(item) != null;
        }

        public void CopyTo(int[] array, int arrayIndex)
        {
            int index = arrayIndex;

            foreach (var v in this)
                array[index++] = v;
        }

        public IEnumerator<int> GetEnumerator()
        {
            var root = mRoot;

            while (root.Valid)
            {
                yield return root.Data;
                root = root.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            var root = mRoot;
            var sb = new StringBuilder();

            while (root.Valid)
            {
                sb.AppendFormat("{0} <=> ", root);
                root = root.Next;
            }

            if (sb.Length > 0)
                sb.Length -= 5;

            return sb.ToString();
        }

        private void Build(int count)
        {
            if (count == 0)
                return;

            // Создаем элемент, который будет стоять до корня.
            //
            var node = new Node(count, mRoot.Previous, mRoot);
            mRoot = (mRoot.Previous = node);

            Build(--count);
        }

        private static Node FindImpl(int value, Node current, ref int? index)
        {
            // Если текущий элемент не нулевой, то проверяем его на соответсвие.
            //
            if (current.Valid)
                if (current.Data != value)
                {
                    ++index;
                    return FindImpl(value, current.Next, ref index);
                }
            else
                index = null;

            return current;
        }

        private void Invalidate()
        {
            mCounted = false;
        }

        private int CountImpl()
        {
            mCount = 0;
            var root = mRoot;

            while (root.Valid)
            {
                ++mCount;
                root = root.Next;
            }

            return mCount;
        }

        // Базовый интерфейс для элемента.
        //
        private class Node
        {
            public Node()
            {

            }

            public Node(int data)
            {
                Data = data;
            }

            public Node(int data, Node previous, Node next)
            {
                Data = data;
                Previous = previous;
                Next = next;
            }

            public override string ToString()
            {
                return Data.ToString();
            }

            public int Data { get; set; }
            public virtual Node Previous { get; internal set; }
            public virtual Node Next { get; internal set; }
            public virtual bool Valid => true;
        }

        // Граничный элемент.
        //
        private class NullNode : Node
        {
            public override string ToString()
            {
                return "nil";
            }

            public override Node Previous
            {
                get { throw new ListEmptyException("Список пуст"); }
                internal set { throw new ListEmptyException("Список пуст"); }
            }
            /* public override Node Next
            {
                get { throw new ListEmptyException("Список пуст"); }
                internal set { throw new ListEmptyException("Список пуст"); }
            } */
            public override bool Valid => false;
        }

        public int Count => mCounted ? mCount : CountImpl();

        public bool IsReadOnly => false;

        private Node mRoot;
        private Node mEnd;
        private int mCount = 0;
        private bool mCounted = false;
    }
}
