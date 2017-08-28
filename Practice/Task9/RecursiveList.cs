using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9
{
    public class RecursiveIntList
    {
        public RecursiveIntList()
        {

        }

        public RecursiveIntList(int data)
        {
            Data = data;
        }

        public RecursiveIntList CreateProgressive(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("Размер должен быть неотрицательным числом.");

            return Cons(count, null);
        }

        public int Head()
        {
            return Data;
        }

        public RecursiveIntList Tail()
        {
            return mTail;
        }

        public int? Find(int element)
        {
            return FindImpl(this, element, 0);
        }

        private static int? FindImpl(RecursiveIntList list, int element, int index)
        {
            int? cur = list?.Head();

            if (!cur.HasValue)
                return null;
            else if (cur.Value == element)
                return index;
            else
                return FindImpl(list?.Tail(), element, ++index);
        }

        private static bool Remove

        private RecursiveIntList Cons(int count, RecursiveIntList rhs)
        {
            var list = new RecursiveIntList() { Data = count, mTail = rhs };

            if (count == 1)
                return list;
            else
                return Cons(count - 1, list);
        }

        public int Data = 0;

        private RecursiveIntList mTail = null;
    }
}
