using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFunctionality
{
    public enum DataType
    {
        IntegerType,
        DoubleType,
        StringType
    }
    public class UIHelpFunctionality
    {
        public static DataType ChosenType(string dataTypePrinted)
        {
            switch (dataTypePrinted)
            {
                case "Integer":
                    return DataType.IntegerType;
                case "Float":
                    return DataType.DoubleType;
                case "Text":
                    return DataType.StringType;
                default:
                    return DataType.StringType;
            }
        }

        public static string Arr2dToStringMatrix<T>(T[,] matrixArr2d, string delimiter = " ")
        {
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
    }
}
