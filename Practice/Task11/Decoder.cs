using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11
{
    public static class Decoder
    {
        public static List<bool> Decode(List<bool> tripledData)
        {
            if (tripledData.Count % 3 != 0)
                throw new ArgumentOutOfRangeException("В контейнере не содержится утроенные сигналы.");

            var result = new List<bool>();

            for (int i = 0; i < tripledData.Count; i += RepetitionsCount)
            {
                int sumTrue = 0;

                for (int j = 0; j < RepetitionsCount; ++j)
                    if (tripledData[i + j])
                        ++sumTrue;
                
                if (sumTrue > RepetitionsCount * .5f)
                    result.Add(true);
                else
                    result.Add(false);
            }

            return result;
        }

        public const int RepetitionsCount = 3;
    }
}
