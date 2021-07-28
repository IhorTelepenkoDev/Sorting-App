using System;
using System.Linq;
using AppFunctionality.ReceiveArrayFromFile;
using AppFunctionality.ReceiveArrayRandomly;

namespace AppFunctionality
{   
    //the supported array2D types are "int", "double", "string" 
    public class ArraySetter<T>
    {
        private readonly T[,] _arr2D;
        public T[,] array2D => _arr2D;

        private IArrayReader<T> arr2DReader;

        // getting array2D from a file:
        public ArraySetter(string filePath)
        {
            string fileExtension = filePath.Substring(filePath.LastIndexOf('.') + 1);

            switch (fileExtension)
            {
                case "json":
                    arr2DReader = new Array2dReaderFromJSON<T>();
                    break;
                default:
                    return;
            }

            _arr2D = arr2DReader.Read2DArray(HelpFunctionalityOfArrReading.ReadFileContent(filePath));
        }

        // assigning the array2D as random:
        public ArraySetter(int lenRows, int lenCols)
        {
            _arr2D = new T[lenRows, lenCols];

            RandomArrayInitializer randInitializer = new RandomArrayInitializer();
            randInitializer.Initialize2DArrayRandomly(_arr2D, lenRows, lenCols);
        }
    }
}
