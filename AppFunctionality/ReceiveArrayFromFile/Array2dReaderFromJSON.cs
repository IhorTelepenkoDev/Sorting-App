using System;
using System.Collections.Generic;
using System.Linq;
using AppFunctionality.Logging;
using Newtonsoft.Json;

namespace AppFunctionality.ReceiveArrayFromFile
{
    internal class ArrayStoringInJSONTemplate
    {
        public string Array { get; set; }
    }

    internal class Array2dReaderFromJSON<T> : IArrayReader<T>
    {
        private readonly ILogger log;

        public Array2dReaderFromJSON()
        {
            log = Logger.GetInstance();
        }

        public T[,] Read2DArray(string dataInJSON)
        {
            T[][] readJaggedArr;

            try
            {
                List<ArrayStoringInJSONTemplate> receivedDataItems =
                    JsonConvert.DeserializeObject<List<ArrayStoringInJSONTemplate>>(dataInJSON);

                readJaggedArr = JsonConvert.DeserializeObject<T[][]>(receivedDataItems.FirstOrDefault().Array);
            }
            catch (JsonSerializationException)
            {
                log.Error($"JSON data is not in the proper format, cannot read the array");
                return null;
            }
            catch (JsonException exception)
            {
                log.Error($"Cannot read the array from JSON: {exception}");
                return null;
            }
                
            
            T[,] readArrIn2D = ArrReadingHelpFunctionality.ConvertFromJaggedTo2DArray(readJaggedArr);
            if(readArrIn2D != null)
                log.Info("Array is successfully read");

            return readArrIn2D;
        }
    }
}
