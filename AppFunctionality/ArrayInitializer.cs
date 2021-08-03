using System;
using System.Linq;
using AppFunctionality.ReceiveArrayFromFile;
using AppFunctionality.ReceiveArrayRandomly;

namespace AppFunctionality
{   
    //the supported array2D types are "int", "double", "string" 
    public class ArrayInitializer<T>
    {
        public T[,] Array2D { get; }

        // getting array2D from a file:
        public ArrayInitializer(string receivedData, IArrayReader<T> arr2DReader)
        {
            Array2D = arr2DReader.Read2DArray(receivedData);
        }

        // assigning the array2D as random:
        public ArrayInitializer(int lenRows, int lenCols)
        {
            Array2D = new T[lenRows, lenCols];

            RandomArrayGenerator randInitializer = new RandomArrayGenerator();
            randInitializer.Generate2DArrayRandomly(Array2D, lenRows, lenCols);
        }
    }
}
