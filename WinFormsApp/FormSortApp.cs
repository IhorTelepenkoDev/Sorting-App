using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Internal;
using System.Diagnostics;
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
        public dynamic BasicArray2D { get; set; } = null;  // may be 2d array of different type
        public Type ArrayElemType { get; set; }

        public FormSortApp()
        {
            InitializeComponent();
            //
        }

        private void buttonArrReadingByPath_Click(object sender, EventArgs e)
        {
            CleanVisibleArrayCharacteristics();
            BasicArray2D = null;

            var pathToFile = textBoxFilePath.Text;
            
            ArrayElemType = HelpFunctionalityOfArrReading.TypeOfArray2DStoredInFile(pathToFile);
            //make ArraySetter as it was before
            if(ArrayElemType != null)
            {
                var contentOfFile = HelpFunctionalityOfArrReading.ReadFileContent(pathToFile);

                if (ArrayElemType == typeof(int))
                    BasicArray2D = new ArrayInitializer<int>(
                                contentOfFile, HelpFunctionalityOfArrReading.ReaderOf2DArrayFromDataSource<int>(pathToFile)).Array2D;
                if (ArrayElemType == typeof(double))
                    BasicArray2D = new ArrayInitializer<double>(
                                contentOfFile, HelpFunctionalityOfArrReading.ReaderOf2DArrayFromDataSource<double>(pathToFile)).Array2D;
                if (ArrayElemType == typeof(string))
                    BasicArray2D = new ArrayInitializer<string>(
                                contentOfFile, HelpFunctionalityOfArrReading.ReaderOf2DArrayFromDataSource<string>(pathToFile)).Array2D;
            }

            if (BasicArray2D == null)
            {
                CleanBasicArrayField();
                return;
            }

            PrintOutput(textBoxBasicArrOutput,
                UIHelpFunctionality.Arr2dToStringMatrix(BasicArray2D, "    "));

        }

        private void buttonRandomArrayAssign_Click(object sender, EventArgs e)
        {
            BasicArray2D = null;

            if (comboBoxDataTypeOfArr.SelectedIndex > 0)
            {
                if (numUpDownRowsInArr.Value > 0 && numUpDownColumnsInArr.Value > 0)
                {
                    int arr2dLengthRows = Convert.ToInt32(numUpDownRowsInArr.Value);
                    int arr2dLengthColumns = Convert.ToInt32(numUpDownColumnsInArr.Value);

                    if(ArrayElemType == typeof(int))
                        BasicArray2D = new ArrayInitializer<int>(arr2dLengthRows, arr2dLengthColumns).Array2D;
                    if (ArrayElemType == typeof(double))
                        BasicArray2D = new ArrayInitializer<double>(arr2dLengthRows, arr2dLengthColumns).Array2D;
                    if (ArrayElemType == typeof(string))
                        BasicArray2D = new ArrayInitializer<string>(arr2dLengthRows, arr2dLengthColumns).Array2D;

                    PrintOutput(textBoxBasicArrOutput,
                        UIHelpFunctionality.Arr2dToStringMatrix(BasicArray2D, "   "));

                    return;
                }
            }

            CleanVisibleArrayCharacteristics();
            CleanBasicArrayField();
        }

        private void comboBoxDataTypeOfArr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxDataTypeOfArr.SelectedIndex != 0)
                ArrayElemType = UIHelpFunctionality.ChosenType(comboBoxDataTypeOfArr.SelectedItem.ToString());
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

        private void CleanBasicArrayField()
        {
            PrintOutput(textBoxBasicArrOutput, null);
        }
    }
}
