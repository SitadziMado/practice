using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(
                "Введите три действительных числа a[1], a[2], a[3] и число M - лимит последовательности: "
            );

            var input = InputSource();

            Console.WriteLine("a[k] = a[k - 1] * a[k - 2] + a[k - 3]");
            Console.WriteLine("Последовательность: ");

            var a1 = input[0];
            var a2 = input[1];
            var a3 = input[2];
            var m = input[3];

            if (a1 == 0.0 && a2 == 0.0 && a3 == 0.0 && m > 0.0)
            {
                Console.WriteLine("Последовательность сходится, но никогда не превышает предела.");
                Console.ReadKey();
                return;
            }

            var sequence = new Sequence(a1, a2, a3, m);
            
            Console.WriteLine(sequence);

            Console.WriteLine("Значение N: {0}", sequence.Count);
            Console.WriteLine("(a[N] ==  M) : {0}", sequence.Last == m);

             Console.ReadKey();
        }

        private static double[] InputSource()
        {
            double[] input;

            while (true)
            {
                try
                {
                    input = (
                        from v
                        in Console.ReadLine().Split()
                        where v.Length > 0
                        select Double.Parse(v)
                    ).ToArray();

                    if (input.Length != 4)
                    {
                        Console.WriteLine("Слишком мало входных параметров, повторите ввод.");
                        continue;
                    }

                    break;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Введенное число слишком большое, попробуйте еще раз.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Некорректное число, повторите ввод.");
                }
            }

            return input;
        }
    }
}
