using System;
using System.IO;
using AppFunctionality.Logging;

namespace AppFunctionality.StoreSortingToFIle
{
    internal class ArrayWritingHelpFunctionality
    {
        private static readonly ILogger log;
        public const string SortingHistoryFileName = "sorting-history.txt";

        static ArrayWritingHelpFunctionality()
        {
            log = Logger.GetInstance();
        }

        public static T[][] ConvertFrom2DArrayToJagged<T>(T[,] array2D)
        {
            try
            {
                int firstDim = array2D.GetLength(0);
                int secondDim = array2D.GetLength(1);

                var jaggedArr = new T[firstDim][];
                for (int i = 0; i < jaggedArr.Length; i++)
                {
                    jaggedArr[i] = new T[secondDim];
                }

                for (int i = 0; i < firstDim; i++)
                for (int j = 0; j < secondDim; j++)
                    jaggedArr[i][j] = array2D[i, j];

                return jaggedArr;
            }
            catch (InvalidOperationException)
            {
                log.Error("The array is not 2-dimensional, cannot be processed");
                return null;
            }
        }

        public static string GetSortingHistoryStoreFilePath()
        {
            string solutionDirectorySortHistoryFile = Path.Combine(Directory.GetParent
                (Directory.GetCurrentDirectory()).Parent.Parent.FullName, SortingHistoryFileName);
            return solutionDirectorySortHistoryFile;
        }
    }
}
