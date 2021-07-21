using System;
using System.Linq;

namespace AppFunctionality.ReceivingArrayFromFile
{
    internal class HelpFunctionalityOfArrReading
    {
        public static T[,] ConvertFromJaggedTo2DArray<T>(T[][] jaggedArray)
        {
            try
            {
                int firstDim = jaggedArray.Length;
                int secondDim = jaggedArray.GroupBy(row => row.Length).Single().Key; // throws InvalidOperationException if jaggedArray is not rectangular

                var array2D = new T[firstDim, secondDim];
                for (int i = 0; i < firstDim; i++)
                for (int j = 0; j < secondDim; j++)
                    array2D[i, j] = jaggedArray[i][j];

                return array2D;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }
}
