using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Internal;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFunctionality;
using AppFunctionality.ReceiveArrayFromFile;

namespace WinFormsApp
{
    public partial class FormSortApp : Form
    {
        public dynamic basicArray2D { get; set; }
        public DataType ArrType { get; set; }

        public FormSortApp()
        {
            InitializeComponent();
        }

        private void buttonArrReadingByPath_Click(object sender, EventArgs e)
        {
            var pathToFile = textBoxFilePath.Text;
            //if (!File.Exists(pathToFile))
                //return;


            ArrType = HelpFunctionalityOfArrReading.TypeOfArray2DStoredInFile(pathToFile);

            switch (ArrType)
            {
                case DataType.IntegerType:
                    basicArray2D = new ArraySetter<int>(pathToFile).array2D;
                    break;
                case DataType.DoubleType:
                    basicArray2D = new ArraySetter<double>(pathToFile).array2D;
                    break;
                default:
                    basicArray2D = new ArraySetter<string>(pathToFile).array2D;
                    break;
            }

            if(basicArray2D==null)
                PrintOutput(textBoxBasicArrOutput, null);
            else
                PrintOutput(textBoxBasicArrOutput,
                    UIHelpFunctionality.Arr2dToStringMatrix(basicArray2D, "    "));

            CleanVisibleArrayCharacteristics();
        }

        private void buttonRandomArrayAssign_Click(object sender, EventArgs e)
        {
            if (comboBoxDataTypeOfArr.SelectedItem != null)
            {
                if (numUpDownRowsInArr.Value != 0 && numUpDownColumnsInArr.Value != 0)
                {
                    int arr2dLengthRows = Convert.ToInt32(numUpDownRowsInArr.Value);
                    int arr2dLengthColumns = Convert.ToInt32(numUpDownColumnsInArr.Value);

                    switch (ArrType)
                    {
                        case DataType.IntegerType:
                            basicArray2D = new ArraySetter<int>(arr2dLengthRows, arr2dLengthColumns).array2D;
                            break;
                        case DataType.DoubleType:
                            basicArray2D = new ArraySetter<double>(arr2dLengthRows, arr2dLengthColumns).array2D;
                            break;
                        default:
                            basicArray2D = new ArraySetter<string>(arr2dLengthRows, arr2dLengthColumns).array2D;
                            break;
                    }

                    
                    PrintOutput(textBoxBasicArrOutput,
                        UIHelpFunctionality.Arr2dToStringMatrix(basicArray2D, "   "));
                    

                }
            }

            CleanVisibleArrayCharacteristics();
        }

        private void comboBoxDataTypeOfArr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxDataTypeOfArr.SelectedIndex != 0)
                ArrType = UIHelpFunctionality.ChosenType(comboBoxDataTypeOfArr.SelectedItem.ToString());
        }

        private static void PrintOutput(TextBox textBoxField, string text)
        {
            //textBoxField.Visible = true;
            textBoxField.Text = text;
        }

        private void CleanVisibleArrayCharacteristics()
        {
            comboBoxDataTypeOfArr.SelectedIndex = 0;
            numUpDownRowsInArr.Value = 0;
            numUpDownColumnsInArr.Value = 0;
        }

        
    }
}
