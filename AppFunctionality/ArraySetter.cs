using System;
using System.Linq;
using AppFunctionality.ReceivingArrayFromFile;

namespace AppFunctionality
{   
    //the supported array2D types are "int", "double", "string" 
    public class ArraySetter<T>
    {
        private readonly T[,] _arr2D;

        public T[,] array2D => _arr2D;

        // getting array2D from a file:
        public ArraySetter(string filePath)
        {
            string fileExtension = filePath.Substring(filePath.LastIndexOf('.') + 1);

            if (fileExtension == "json")
            {
                Array2dReaderFromJSON<T> arrReaderFromJSON = new Array2dReaderFromJSON<T>(filePath);

                _arr2D = arrReaderFromJSON.Receive2DArrayFromFile();
            }

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
