using System;
using System.IO;
using AppFunctionality.Logging;
using Newtonsoft.Json;

namespace AppFunctionality.StoreSortingToFIle
{
    public class jsonSorting
    {
        public string sorter { get; set; }
        public string unsortedArray { get; set; }
        public string sortedArray { get; set; }
        public string date { get; set; }
    }

    public class SortingWriterToJSONFile
    {
        private readonly ILogger log;
        private string storeFilePath;

        public SortingWriterToJSONFile(string filePath = null)
        {
            log = Logger.GetInstance();

            if (filePath == null)
                storeFilePath = ArrayWritingHelpFunctionality.GetSortingHistoryStoreFilePath();
            else
                storeFilePath = filePath;
        }

        public void StoreSortingData(string sorter, dynamic unsortedArray2d, dynamic sortedArray2d, DateTime date)
        {
            var jaggedUnsortedArray = ArrayWritingHelpFunctionality.ConvertFrom2DArrayToJagged(unsortedArray2d);
            var unsortedArrayJSONFormat = JsonConvert.SerializeObject(jaggedUnsortedArray);
            var jaggedSortedArray = ArrayWritingHelpFunctionality.ConvertFrom2DArrayToJagged(sortedArray2d);
            var sortedArrayJSONFormat = JsonConvert.SerializeObject(jaggedSortedArray);

            var sortingDataToBeStored = new jsonSorting[]
            {
                new jsonSorting()
                {
                    sorter = sorter,
                    unsortedArray = unsortedArrayJSONFormat,
                    sortedArray = sortedArrayJSONFormat,
                    date = date.ToShortDateString()
                }
            };

            string sortingToBeStoredJSON = JsonConvert.SerializeObject(sortingDataToBeStored);

            try
            {
                File.AppendAllText(storeFilePath, sortingToBeStoredJSON + Environment.NewLine);
            }
            catch (DirectoryNotFoundException)
            {
                log.Error("Wrong history file location, sorting data cannot be stored");
            }
            catch (Exception exception)
            {
                log.Error($"Cannot store sorting history into file: {exception}");
            }
        }

    }
}
