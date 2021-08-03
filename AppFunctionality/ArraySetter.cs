using System;
using System.Linq;
using AppFunctionality.ReceiveArrayFromFile;
using AppFunctionality.ReceiveArrayRandomly;

namespace AppFunctionality
{   
    //the supported array2D types are "int", "double", "string" 
    public class ArraySetter<T>
    {
        public T[,] array2D { get; }

        // getting array2D from a file:
        public ArraySetter(string receivedData, IArrayReader<T> arr2DReader)
        {
            array2D = arr2DReader.Read2DArray(receivedData);
        }

        // assigning the array2D as random:
        public ArraySetter(int lenRows, int lenCols)
        {
            array2D = new T[lenRows, lenCols];

            RandomArrayInitializer randInitializer = new RandomArrayInitializer();
            randInitializer.Initialize2DArrayRandomly(array2D, lenRows, lenCols);
        }
    }
}
