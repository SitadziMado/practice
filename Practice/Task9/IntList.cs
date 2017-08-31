using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9
{
    public class IntList
    {
        private IntList()
        {
            mRoot = new NullNode() { Previous = new NullNode() };
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

                return true;
            }
            else
            {
                return false;
            }
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

            public int Data { get; set; }
            public virtual Node Previous { get; internal set; }
            public virtual Node Next { get; internal set; }
            public virtual bool Valid => true;
        }

        private class NullNode : Node
        {
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

        private Node mRoot;
    }
}
