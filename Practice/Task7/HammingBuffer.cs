using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7
{
    public class HammingBuffer : IReadOnlyCollection<bool>
    {
        public HammingBuffer()
        {

        }

        public HammingBuffer(List<bool> data)
        {
            Init(data);
        }

        public HammingBuffer(IEnumerable<bool> data)
        {
            Init(data.ToList());
        }
        
        public IEnumerator<bool> GetEnumerator()
        {
            foreach (var v in mData)
            {
                yield return v;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return String.Join(
                "", 
                from v 
                in this
                where true
                select v ? "1" : "0"
            );
        }

        private void Init(List<bool> data)
        {
            int cur = 2;

            mData.AddRange(new bool[] { false, false }.AsEnumerable());

            foreach (var v in data)
            {
                if (Utility.IsPowerOfTwo(cur + 1))
                {
                    mData.Add(false);
                    ++cur;
                }

                mData.Add(v);

                ++cur;
            }

            SetControlBits();
        }

        private void SetControlBits()
        {
            for (int i = 1; i < mData.Count; i <<= 1)
            {
                bool control = false;

                for (int j = i - 1; j < mData.Count; j += 2 * i)
                    for (int k = j; k < j + i && k < mData.Count; ++k)
                        control ^= mData[k];

                mData[i - 1] = control;
            }
        }

        private List<bool> mData = new List<bool>();

        public int Count => mData.Count;
    }
}
