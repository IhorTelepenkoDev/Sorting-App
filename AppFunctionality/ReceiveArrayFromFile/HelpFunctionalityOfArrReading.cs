using System;
using System.IO;
using System.Linq;

namespace AppFunctionality.ReceiveArrayFromFile
{
    public class HelpFunctionalityOfArrReading
    {
        public static string ReadFileContent(string filePath)
        {
            string fileContent = null;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                    fileContent = reader.ReadToEnd();
            }
            catch {}
            
            return fileContent;
        }

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

        public static DataType TypeOfArray2DStoredInFile(string filePath)
        {
            var contentOfFile = ReadFileContent(filePath);
            string fileExtension = filePath.Substring(filePath.LastIndexOf('.') + 1);

            switch (fileExtension)
            {
                case "json":
                {
                    if (new Array2dReaderFromJSON<int>().Read2DArray(contentOfFile) != null)
                        return DataType.IntegerType;
                    if (new Array2dReaderFromJSON<double>().Read2DArray(contentOfFile) != null)
                        return DataType.DoubleType;
                    return DataType.StringType;
                }
                default:
                    return DataType.StringType;

            }
        }


        //private static bool IsReadingOfArrayFromDataPossible(Delegate arrayReadingOperation, string checkedData)
        //{
        //    var readResult = arrayReadingOperation.DynamicInvoke(checkedData);

        //    return readResult != null;
        //}
    }
}
