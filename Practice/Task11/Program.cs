﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11
{
    class Program
    {
        private const double NoiseChance = .1;
        private static List<bool> mData = new List<bool>();
        private static Random mRnd = new Random();

        static void Main(string[] args)
        {
            string input;

            Console.WriteLine("Введите массив битов для \"отправки\": ");

            do
            {
                input = Console.ReadLine();
            } while (!ParseBinaryArray(input));

            Console.WriteLine("Сгенерированные \"помехи\" (вероятность ошибки: {0}%): ", (int)(NoiseChance * 100.0));

            var noiseData = GenerateNoise();
            PrintNoise(noiseData);

            Console.WriteLine("\nВосстановленный (по возможности) результат: ");
            Print(Decoder.Decode(noiseData));
        }

        private static bool ParseBinaryArray(string data)
        {
            mData.Clear();

            foreach (var v in data)
            {
                if (v == '0')
                    mData.Add(false);
                else if (v == '1')
                    mData.Add(true);
                else
                    return false;
            }

            return true;
        }

        private static List<bool> GenerateNoise()
        {
            var list = new List<bool>();

            foreach (var v in mData)
            {
                list.Add(v);
                list.Add((mRnd.NextDouble() > NoiseChance) ? (v) : (!v));
                list.Add((mRnd.NextDouble() > NoiseChance) ? (v) : (!v));
            }

            return list;
        }

        private static void Print(List<bool> list)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            for (int i = 0; i < list.Count; ++i)
            {
                bool original = mData[i];
                bool noise = list[i];

                if (original == noise)
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                else
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                if (noise)
                    Console.Write('1');
                else
                    Console.Write('0');
            }

            Console.WriteLine();
            Console.ResetColor();
        }

        private static void PrintNoise(List<bool> list)
        {
            for (int i = 0; i < list.Count; i += Decoder.RepetitionsCount)
            {
                bool original = list[i];
                bool firstNoise = list[i + 1];

                if (original == firstNoise)
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                else
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                if (firstNoise)
                    Console.Write('1');
                else
                    Console.Write('0');
            }

            Console.WriteLine();

            for (int i = 0; i < list.Count; i += Decoder.RepetitionsCount)
            {
                bool original = list[i];
                bool secondNoise = list[i + 2];

                if (original == secondNoise)
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                else
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                if (secondNoise)
                    Console.Write('1');
                else
                    Console.Write('0');
            }

            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
