using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12
{
    class Program
    {
        private static IEnumerable<int> mRange;
        private static int[] mAscending;
        private static int[] mDescending;
        private static int[] mScattered;

        static void Main(string[] args)
        {
            int size;

            Console.WriteLine("Введите натуральное число - размер:");

            while (!Int32.TryParse(Console.ReadLine(), out size) && size <= 0)
                Console.WriteLine("Введите натуральное число.");

            SortAndPrintStatistics(size);
            Console.ReadKey();
        }

        private static int[] Shuffle(int[] array)
        {
            Random rnd = new Random();
            return array.OrderBy(x => rnd.Next()).ToArray();
        }

        private static void SortAndPrintStatistics(int size)
        {
            mRange = from v in Enumerable.Range(0, size) where true select v;
            mAscending = mRange.ToArray();
            mDescending = mRange.Reverse().ToArray();
            mScattered = mRange.ToArray();
            
            Console.WriteLine("Были созданы и отсортированы целочисленные диапазоны от {0} то {1}.", 0, size);
            Console.WriteLine("Размер каждого из диапазонов: {0}.", mRange.Count());

            SortAndPrintTrees();
            SortAndPrintArrays(size);
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

        private static void SortAndPrintArrays(int size)
        {
            var ascendingArray = mAscending.CountingSort(0, size);
            var ascendingStat = Utility.LastMemoryAccessCount;
            var descendingArray = mDescending.CountingSort(0, size);
            var descendingStat = Utility.LastMemoryAccessCount;
            var scatteredArray = mScattered.CountingSort(0, size);
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
