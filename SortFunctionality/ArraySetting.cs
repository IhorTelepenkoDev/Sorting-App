using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortFunctionality
{   //the supported array types are "int", "double", "string" 
    public class ArraySetting<T>
    {
        private T[,] _arr;
        private int _lenRows, _lenColumns;

        private static readonly Random rnd = new Random();
        public T[,] array => _arr;

        // getting array from a file:
        public ArraySetting(string fileName)
        {

        }

        // assigning the array as random:
        public ArraySetting(int lenRows, int lenCols)
        {
            _lenRows = lenRows;
            _lenColumns = lenCols;
            _arr = new T[_lenRows, _lenColumns];

            RandomArrayInit(_arr, _lenRows, _lenColumns);
        }

        private static void RandomArrayInit(T[,] arr, int lenRows, int lenCols)
        {
            if (typeof(T) == typeof(string))
            {
                for (int i = 0; i < lenRows; i++)
                    for (int j = 0; j < lenCols; j++)
                    {
                        int textLen = rnd.Next(1, 11);
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
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, len)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        private static int RandInt()
        {
            return rnd.Next(int.MinValue / 1000, int.MaxValue / 1000);
        }

        private static double RandDouble()
        {
            var number = rnd.NextDouble() * 2d - 1d;
            return number * int.MaxValue / 1000;
        }
    }
}
