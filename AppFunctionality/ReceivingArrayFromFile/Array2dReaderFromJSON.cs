using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace AppFunctionality.ReceivingArrayFromFile
{
    internal class TemplateOfArrayStoringInJSON
    {
        public string array { get; set; }
    }

    internal class Array2dReaderFromJSON<T> : IArrayReaderFromFile<T>
    {
        public string filePath { get; }

        public Array2dReaderFromJSON(string pathToJsonFile)
        {
            filePath = pathToJsonFile;
        }

        public T[,] Receive2DArrayFromFile()
        {
            T[][] receivedJaggedArr;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string allReadDataJSON = reader.ReadToEnd();
                try
                {
                    List<TemplateOfArrayStoringInJSON> receivedDataItems =
                        JsonConvert.DeserializeObject<List<TemplateOfArrayStoringInJSON>>(allReadDataJSON);

                    receivedJaggedArr = JsonConvert.DeserializeObject<T[][]>(receivedDataItems.FirstOrDefault().array);
                }
                catch
                {
                    return null;
                }
                
            }

            T[,] receivedArrIn2D = HelpFunctionalityOfArrReading.ConvertFromJaggedTo2DArray(receivedJaggedArr);

            return receivedArrIn2D;
        }
    }
}
