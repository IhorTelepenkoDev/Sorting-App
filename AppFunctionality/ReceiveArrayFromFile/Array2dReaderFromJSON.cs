using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace AppFunctionality.ReceiveArrayFromFile
{
    internal class ArrayStoringInJSONTemplate
    {
        public string Array { get; set; }
    }

    internal class Array2dReaderFromJSON<T> : IArrayReader<T>
    {
        public T[,] Read2DArray(string dataInJSON)
        {
            T[][] readJaggedArr;

            try
            {
                List<ArrayStoringInJSONTemplate> receivedDataItems =
                    JsonConvert.DeserializeObject<List<ArrayStoringInJSONTemplate>>(dataInJSON);

                readJaggedArr = JsonConvert.DeserializeObject<T[][]>(receivedDataItems.FirstOrDefault().Array);
            }
            catch
            {
                return null;
            }
                
            
            T[,] readArrIn2D = ArrReadingHelpFunctionality.ConvertFromJaggedTo2DArray(readJaggedArr);

            return readArrIn2D;
        }
    }
}
