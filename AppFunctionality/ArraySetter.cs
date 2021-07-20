using System;
using System.Linq;

namespace AppFunctionality
{   
    //the supported array2D types are "int", "double", "string" 
    public class ArraySetter<T>
    {
        private readonly T[,] _arr2D;
        private readonly int _lenRows, _lenColumns;

        public T[,] array2D => _arr2D;

        // getting array2D from a file:
        public ArraySetter(string fileName)
        {

        }

        // assigning the array2D as random:
        public ArraySetter(int lenRows, int lenCols)
        {
            _lenRows = lenRows;
            _lenColumns = lenCols;
            _arr2D = new T[_lenRows, _lenColumns];

            RandomInitializer randInitializer = new RandomInitializer();
            randInitializer.Initialize2DArrayRandomly(_arr2D, _lenRows, _lenColumns);
        }
    }
}
