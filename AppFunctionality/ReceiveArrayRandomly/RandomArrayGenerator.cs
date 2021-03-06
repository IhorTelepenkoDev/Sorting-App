using System;
using System.Linq;

namespace AppFunctionality.ReceiveArrayRandomly
{
    internal class RandomArrayGenerator
    {
        //the supported array2D types are "int", "double", "string"
        private static readonly Random rand = new Random();

        public void Generate2DArrayRandomly<T>(T[,] arr, int lenRows, int lenCols)
        {
            if (typeof(T) == typeof(string))
            {
                const int maxStringLength = 15;

                for (int i = 0; i < lenRows; i++)
                    for (int j = 0; j < lenCols; j++)
                    {
                        int textLen = rand.Next(1, maxStringLength);
                        arr[i, j] = (T)Convert.ChangeType(GenerateRandString(textLen), typeof(T));
                    }
            }
            else if (typeof(T) == typeof(int))
            {
                for (int i = 0; i < lenRows; i++)
                    for (int j = 0; j < lenCols; j++)
                    {
                        arr[i, j] = (T)Convert.ChangeType(GenerateRandInteger(), typeof(T));
                    }
            }
            else if (typeof(T) == typeof(double))
            {
                for (int i = 0; i < lenRows; i++)
                    for (int j = 0; j < lenCols; j++)
                    {
                        arr[i, j] = (T)Convert.ChangeType(GenerateRandDouble(), typeof(T));
                    }
            }
        }

        private static string GenerateRandString(int len)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, len)
                .Select(s => s[rand.Next(s.Length)]).ToArray());
        }

        private static int GenerateRandInteger()
        {
            var rangeReducer = GetRangeReduceCoefficient();
            return rand.Next(int.MinValue / rangeReducer, int.MaxValue / rangeReducer);
        }

        private static double GenerateRandDouble()
        {
            var rangeReducer = GetRangeReduceCoefficient();
            var number = rand.NextDouble() * 2d - 1d;
            return number * int.MaxValue / rangeReducer;
        }

        private static int GetRangeReduceCoefficient()
        {
            int reduceCoeff = 1;
            bool isRangeReduce = rand.Next(0, 2) > 0;
            if (isRangeReduce)
                reduceCoeff = 1000;

            return reduceCoeff;
        }
    }
}
