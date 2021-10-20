using System;
using System.Text;

namespace AppFunctionality
{
    public class ArrayHelpFunctionality
    {
        public static Type GetSelectedArrType(string dataTypePrinted)
        {
            switch (dataTypePrinted)
            {
                case "Integer":
                    return typeof(int);
                case "Float":
                    return typeof(double);
                case "Text":
                    return typeof(string);
                default:
                    return null;
            }
        }

        public static string Arr2dToString<T>(T[,] matrixArr2d, string delimiter = " ")
        {
            if (matrixArr2d == null)
                return null;

            var strBuilder = new StringBuilder();

            for (var i = 0; i < matrixArr2d.GetLength(0); i++)
            {
                for (var j = 0; j < matrixArr2d.GetLength(1); j++)
                {
                    strBuilder.Append(matrixArr2d[i, j]).Append(delimiter);
                }
                strBuilder.AppendLine();
            }

            return strBuilder.ToString();
        }

        public static dynamic Copy2dArr(dynamic array2d, Type arrType)
        {
            dynamic resultCopiedArr2d = null;
            try
            {
                if (arrType == typeof(int))
                    resultCopiedArr2d = new int[array2d.GetLength(0), array2d.GetLength(1)];
                else
                if (arrType == typeof(double))
                    resultCopiedArr2d = new double[array2d.GetLength(0), array2d.GetLength(1)];
                else resultCopiedArr2d = new string[array2d.GetLength(0), array2d.GetLength(1)];

                Array.Copy(array2d, resultCopiedArr2d, array2d.GetLength(0) * array2d.GetLength(1));
            }
            catch { }

            return resultCopiedArr2d;
        }
    }
}
