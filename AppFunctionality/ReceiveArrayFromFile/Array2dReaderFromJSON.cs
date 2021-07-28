using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace AppFunctionality.ReceiveArrayFromFile
{
    internal class TemplateOfArrayStoringInJSON
    {
        public string array { get; set; }
    }

    internal class Array2dReaderFromJSON<T> : IArrayReader<T>
    {
        public T[,] Read2DArray(string dataInJSON)
        {
            T[][] readJaggedArr;

            try
            {
                List<TemplateOfArrayStoringInJSON> receivedDataItems =
                    JsonConvert.DeserializeObject<List<TemplateOfArrayStoringInJSON>>(dataInJSON);

                readJaggedArr = JsonConvert.DeserializeObject<T[][]>(receivedDataItems.FirstOrDefault().array);
            }
            catch
            {
                return null;
            }
                
            
            T[,] readArrIn2D = HelpFunctionalityOfArrReading.ConvertFromJaggedTo2DArray(readJaggedArr);

            return readArrIn2D;
        }
    }
}
