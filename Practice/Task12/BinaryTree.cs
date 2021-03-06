﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12
{
    public class BinaryTree<T> : ICollection<T>
    {
        public BinaryTree()
        {

        }

        public BinaryTree(IEnumerable<T> data)
        {
            foreach (var v in data)
            {
                Add(v);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var queue = new Queue<T>();

            Dfs(queue, mRoot);

            while (queue.Count > 0)
            {
                yield return queue.Dequeue();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (mRoot == null)
            {
                mRoot = new Node(item);
                ++MemoryAccessCount;        // Счетчик доступов к памяти.
            }
            else
            {
                Node cur = mRoot;

                while (true)
                {
                    int cmp = Compare(item, cur.Data);

                    if (cmp == -1)
                        if (cur.Left != null)
                        {
                            ++MemoryAccessCount;        // Счетчик доступов к памяти.
                            cur = cur.Left;
                        }
                        else
                        {
                            ++MemoryAccessCount;        // Счетчик доступов к памяти.
                            cur.Left = new Node(item);
                            break;
                        }
                    else if (cmp == +1)
                        if (cur.Right != null)
                        {
                            ++MemoryAccessCount;        // Счетчик доступов к памяти.
                            cur = cur.Right;
                        }
                        else
                        {
                            ++MemoryAccessCount;        // Счетчик доступов к памяти.
                            cur.Right = new Node(item);
                            break;
                        }
                    else
                        return;
                }
            }

            ++Count;
        }

        public void Clear()
        {
            mRoot = null;
        }

        public bool Contains(T item)
        {
            foreach (var v in this)
            {
                if (Compare(item, v) == 0)
                    return true;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            //array = new T [Count];

            int index = arrayIndex;

            foreach (var v in this)
                array[index++] = v;
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        private void Dfs(Queue<T> queue, Node cur)
        {
            ++DfsEntryCount;

            if (cur == null)
                return;

            Dfs(queue, cur.Left);
            queue.Enqueue(cur.Data);
            Dfs(queue, cur.Right);
        }

        private int Compare(T lhs, T rhs)
        {
            ++ComparisonCount;
            return mComparer.Compare(lhs, rhs);
        }

        private class Node
        {
            public Node(T data)
            {
                Data = data;
            }

            public override string ToString()
            {
                return Data.ToString();
            }

            public T Data { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public int Count { get; set; }
        public bool IsReadOnly => false;
        public int ComparisonCount { get; set; }
        public int DfsEntryCount { get; set; }
        public int MemoryAccessCount { get; set; }

        private Comparer<T> mComparer = Comparer<T>.Default;
        private Node mRoot = null;
    }
}
