using System;
using System.Linq;

namespace AppFunctionality.ReceiveArrayRandomly
{
    internal class RandomArrayGenerator
    {
        //the supported array2D types are "int", "double", "string"
        private static readonly Random rnd = new Random();

        public void Generate2DArrayRandomly<T>(T[,] arr, int lenRows, int lenCols)
        {
            if (typeof(T) == typeof(string))
            {
                const int maxStringLength = 15;

                for (int i = 0; i < lenRows; i++)
                    for (int j = 0; j < lenCols; j++)
                    {
                        int textLen = rnd.Next(1, maxStringLength);
                        arr[i, j] = (T)Convert.ChangeType(RandStr(textLen), typeof(T));
                    }
            }
            else if (typeof(T) == typeof(int))
            {
                for (int i = 0; i < lenRows; i++)
                    for (int j = 0; j < lenCols; j++)
                    {
                        arr[i, j] = (T)Convert.ChangeType(RandInt(), typeof(T));
                    }
            }
            else if (typeof(T) == typeof(double))
            {
                for (int i = 0; i < lenRows; i++)
                    for (int j = 0; j < lenCols; j++)
                    {
                        arr[i, j] = (T)Convert.ChangeType(RandDouble(), typeof(T));
                    }
            }
        }

        private static string RandStr(int len)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, len)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        private static int RandInt()
        {
            var rangeReducer = RangeReduceCoefficient();
            return rnd.Next(int.MinValue / rangeReducer, int.MaxValue / rangeReducer);
        }

        private static double RandDouble()
        {
            var rangeReducer = RangeReduceCoefficient();
            var number = rnd.NextDouble() * 2d - 1d;
            return number * int.MaxValue / rangeReducer;
        }

        private static int RangeReduceCoefficient()
        {
            int reduceCoeff = 1;
            bool isRangeReduce = rnd.Next(0, 2) > 0;
            if (isRangeReduce)
                reduceCoeff = 1000;

            return reduceCoeff;
        }
    }
}
