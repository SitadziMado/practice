using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12
{
    public static class Utility
    {
        public static IEnumerable<int> CountingSort(this IEnumerable<int> range, int minValue, int maxValue)
        {
            try
            {
                if (minValue > maxValue)
                    throw new ArgumentOutOfRangeException("Минимальное значение не должно превышать максимальное.");

                // if (maxValue - minValue > CountingSortLimit)
                //     throw new ArgumentOutOfRangeException("Слишком большой диапазон чисел.");

                int size = range.Count();
                int[] result = new int[size];
                int[] count = new int[maxValue - minValue + 1];

                LastMemoryAccessCount = 0;

                foreach (var v in range)
                {
                    ++count[v - minValue];
                    ++LastMemoryAccessCount;
                }

                int index = 0;

                for (int i = minValue; i <= maxValue; ++i)
                    for (int j = count[i - minValue]; j-- != 0;)
                    {
                        result[index++] = i;
                        ++LastMemoryAccessCount;
                    }

                return result.AsEnumerable();
            }
            catch (OutOfMemoryException e)
            {
                throw new OutOfMemoryException("Слишком большой диапазон чисел.", e);
            }
        }

        public const int CountingSortLimit = 1000000;

        public static int LastMemoryAccessCount { get; set; }
    }
}
