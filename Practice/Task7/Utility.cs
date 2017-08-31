using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7
{
    public static class Utility
    {
        public static bool IsPowerOfTwo(int number)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException(
                    "Число должно быть неотрицательным."
                );

            return (number & (number - 1)) == 0;
        }
    }
}
