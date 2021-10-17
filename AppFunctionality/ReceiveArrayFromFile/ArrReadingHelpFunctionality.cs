using System;
using System.IO;
using System.Linq;
using AppFunctionality.Logging;

namespace AppFunctionality.ReceiveArrayFromFile
{
    public class ArrReadingHelpFunctionality
    {
        private static readonly Logger log = Logger.GetInstance();

        public static string ReadFileContent(string filePath)
        {
            string fileContent = null;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                    fileContent = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                log.Error($"File ({filePath}) cannot be read: {e}");
            }
            
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
                log.Error("The array is not 2-dimensional, cannot be read");
                return null;
            }
        }

        public static Type GetTypeOfArray2DStoredInFile(string filePath)
        {
            var contentOfFile = ReadFileContent(filePath);
            string fileExtension = filePath.Substring(filePath.LastIndexOf('.') + 1);

            switch (fileExtension)
            {
                case "json":
                {
                    if (new Array2dReaderFromJSON<int>().Read2DArray(contentOfFile) != null)
                        return typeof(int);
                    if (new Array2dReaderFromJSON<double>().Read2DArray(contentOfFile) != null)
                        return typeof(double);
                    return typeof(string);
                }
                default:
                {
                    log.Warn($"Files '.{fileExtension}' are not supported, file cannot be read");
                    return null;
                }

            }
        }

        public static IArrayReader<T> Get2DArrayReaderFromDataSource<T>(string dataSourcePath)
        {
            string dataSourceExtension = dataSourcePath.Substring(dataSourcePath.LastIndexOf('.') + 1);

            switch (dataSourceExtension)
            {
                case "json":
                    return new Array2dReaderFromJSON<T>();
                default:
                {
                    log.Error($"Format '.{dataSourcePath}' is not supported for reading");
                    return null;
                }
            }

        }
    }
}
