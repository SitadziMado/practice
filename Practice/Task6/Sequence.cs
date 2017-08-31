using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    public class Sequence : IReadOnlyCollection<double>
    {
        public Sequence(double first, double second, double third, double limit)
        {
            mData.AddLast(first);

            if (first < limit)
            {
                mData.AddLast(second);

                if (second < limit)
                {
                    mData.AddLast(third);
                    Construct(first, second, third, limit);
                }
            }
        }

        private void Construct(double last, double beforeLast, double beforeBeforeLast, double limit)
        {
            if (last > limit)
                return;

            double current = last * beforeLast + beforeBeforeLast;
            mData.AddLast(current);
            Construct(current, last, beforeLast, limit);
        }

        public IEnumerator<double> GetEnumerator()
        {
            return mData.GetEnumerator();
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

            if (sb.Length > 0)
                sb.Length -= 4;

            return sb.ToString();
        }

        public double Last => mData.Last.Value;
        public int Count => mData.Count;

        private LinkedList<double> mData = new LinkedList<double>();
    }
}
