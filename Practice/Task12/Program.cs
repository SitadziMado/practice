using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12
{
    class Program
    {
        private const int Multiplier = 32;

        private const int RawMinValue = -1024;
        private const int RawMaxValue = -RawMinValue;

        private const int MinValue = RawMinValue * Multiplier;
        private const int MaxValue = -MinValue;

        private static IEnumerable<int> mRange = from v in Enumerable.Range(RawMinValue, RawMaxValue) where true select v * Multiplier;
        private static int[] mAscending = mRange.ToArray();
        private static int[] mDescending = mRange.Reverse().ToArray();
        private static int[] mScattered = mRange.ToArray();

        static void Main(string[] args)
        {
            SortAndPrintStatistics();
            Console.ReadKey();
        }

        private static int[] Shuffle(int[] array)
        {
            Random rnd = new Random();
            return array.OrderBy(x => rnd.Next()).ToArray();
        }

        private static void SortAndPrintStatistics()
        {

            Console.WriteLine("Были созданы и отсортированы целочисленные диапазоны от {0} то {1}.", MinValue, MaxValue);

            SortAndPrintTrees();
            SortAndPrintArrays();
        }

        private static void SortAndPrintTrees()
        {
            mScattered = Shuffle(mScattered);

            var ascendingTree = new BinaryTree<int>(mAscending);
            var descendingTree = new BinaryTree<int>(mDescending);
            var scatteredTree = new BinaryTree<int>(mScattered);

            Console.WriteLine(
                "Количество сравнений, доступов к памяти, рекурсивных спусков для метода сортировки с помощью двоичного дерева: "
            );
            Console.WriteLine(
                "\tВозрастающий список: {0}, {1}, {2}.",
                ascendingTree.ComparisonCount,
                ascendingTree.MemoryAccessCount,
                ascendingTree.DfsEntryCount
            );
            Console.WriteLine(
                "\tУбывающий список: {0}, {1}, {2}.",
                descendingTree.ComparisonCount,
                descendingTree.MemoryAccessCount,
                descendingTree.DfsEntryCount
            );
            Console.WriteLine(
                "\tНеупорядоченный список: {0}, {1}, {2}.",
                scatteredTree.ComparisonCount,
                scatteredTree.MemoryAccessCount,
                scatteredTree.DfsEntryCount
            );

            Console.WriteLine();
        }

        private static void SortAndPrintArrays()
        {
            var ascendingArray = mAscending.CountingSort(MinValue, MaxValue);
            var ascendingStat = Utility.LastMemoryAccessCount;
            var descendingArray = mDescending.CountingSort(MinValue, MaxValue);
            var descendingStat = Utility.LastMemoryAccessCount;
            var scatteredArray = mScattered.CountingSort(MinValue, MaxValue);
            var scatteredStat = Utility.LastMemoryAccessCount;
            
            Console.WriteLine(
                "Количество доступов к памяти для метода сортировки подсчетом: "
            );
            
            Console.WriteLine("\tВозрастающий список: {0}.", ascendingStat);
            Console.WriteLine("\tУбывающий список: {0}.", descendingStat);
            Console.WriteLine("\tНеупорядоченный список: {0}.", scatteredStat);

            Console.WriteLine();
        }
    }
}
