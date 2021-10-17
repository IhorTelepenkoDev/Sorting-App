using System;
using System.Linq;
using AppFunctionality.ReceiveArrayFromFile;
using AppFunctionality.ReceiveArrayRandomly;
using AppFunctionality.Logging;

namespace AppFunctionality
{   
    //the supported array2D types are "int", "double", "string" 
    public class ArrayInitializer<T>
    {
        public T[,] Array2D { get; }
        private readonly Logger log = Logger.GetInstance();

        // getting array2D from a file:
        public ArrayInitializer(string receivedData, IArrayReader<T> arr2DReader)
        {
            Array2D = arr2DReader.Read2DArray(receivedData);
            log.Info("Array reading from file is finished");
        }

        // assigning the array2D as random:
        public ArrayInitializer(int lenRows, int lenCols)
        {
            Array2D = new T[lenRows, lenCols];

            RandomArrayGenerator randInitializer = new RandomArrayGenerator();
            randInitializer.Generate2DArrayRandomly(Array2D, lenRows, lenCols);
            log.Info("Array is randomly generated");
        }
    }
}
