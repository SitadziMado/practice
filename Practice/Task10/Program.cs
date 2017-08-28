using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10
{
    class Program
    {
        private static int mCount;
        private static int mIndex;
        private static LoopedList<int> mList;

        static void Main(string[] args)
        {
            InputData();

            mList = new LoopedList<int>(Enumerable.Range(1, mCount));

            int index = mIndex - 1;

            while (mList.Count > 1)
            {
                index = mList.RemoveAt(index) + mIndex - 1;
            }

            Console.WriteLine("Номер оставшегося человека: {0}.", mList.First());
        }

        private static void InputData()
        {
            InputCount();
            InputIndex();
        }

        private static void InputCount()
        {
            Console.WriteLine("Введите количество людей в круге (число N): ");

            while (true)
            {
                try
                {
                    mCount = Int32.Parse(Console.ReadLine());

                    if (mCount <= 0)
                        Console.WriteLine("Число должно быть натуральным.");
                    else
                        break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Требуется число.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Слишком большое число. Попробуйте еще раз: ");
                }
            }
        }

        private static void InputIndex()
        {
            Console.WriteLine("Введите номер человека, который будет выходить из круга (число M): ");

            while (true)
            {
                try
                {
                    mIndex = Int32.Parse(Console.ReadLine());

                    if (mIndex <= 0)
                        Console.WriteLine("Число должно быть натуральным.");
                    else
                        break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Требуется число.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Слишком большое число. Попробуйте еще раз: ");
                }
            }
        }
    }
}
