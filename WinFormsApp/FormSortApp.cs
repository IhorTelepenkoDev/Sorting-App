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
        public dynamic basicArray2D { get; set; } = null;  // may be 2d array of different type
        public Type ArrType { get; set; }

        public FormSortApp()
        {
            InitializeComponent();
            //
        }

        private void buttonArrReadingByPath_Click(object sender, EventArgs e)
        {
            CleanVisibleArrayCharacteristics();
            basicArray2D = null;

            var pathToFile = textBoxFilePath.Text;
            var contentOfFile = HelpFunctionalityOfArrReading.ReadFileContent(pathToFile);
            
            ArrType = HelpFunctionalityOfArrReading.TypeOfArray2DStoredInFile(pathToFile);
            //make ArraySetter as it was before
            if(ArrType != null)
            {
                if(ArrType == typeof(int))
                    basicArray2D = new ArraySetter<int>(
                                contentOfFile, HelpFunctionalityOfArrReading.ReaderOf2DArrayFromDataSource<int>(pathToFile)).array2D;
                if (ArrType == typeof(double))
                    basicArray2D = new ArraySetter<double>(
                                contentOfFile, HelpFunctionalityOfArrReading.ReaderOf2DArrayFromDataSource<double>(pathToFile)).array2D;
                if (ArrType == typeof(string))
                    basicArray2D = new ArraySetter<string>(
                                contentOfFile, HelpFunctionalityOfArrReading.ReaderOf2DArrayFromDataSource<string>(pathToFile)).array2D;
            }

            if (basicArray2D == null)
            {
                CleanBasicArrayField();
                return;
            }

            PrintOutput(textBoxBasicArrOutput,
                UIHelpFunctionality.Arr2dToStringMatrix(basicArray2D, "    "));

        }

        private void buttonRandomArrayAssign_Click(object sender, EventArgs e)
        {
            basicArray2D = null;

            if (comboBoxDataTypeOfArr.SelectedIndex > 0)
            {
                if (numUpDownRowsInArr.Value > 0 && numUpDownColumnsInArr.Value > 0)
                {
                    int arr2dLengthRows = Convert.ToInt32(numUpDownRowsInArr.Value);
                    int arr2dLengthColumns = Convert.ToInt32(numUpDownColumnsInArr.Value);

                    if(ArrType == typeof(int))
                        basicArray2D = new ArraySetter<int>(arr2dLengthRows, arr2dLengthColumns).array2D;
                    if (ArrType == typeof(double))
                        basicArray2D = new ArraySetter<double>(arr2dLengthRows, arr2dLengthColumns).array2D;
                    if (ArrType == typeof(string))
                        basicArray2D = new ArraySetter<string>(arr2dLengthRows, arr2dLengthColumns).array2D;

                    PrintOutput(textBoxBasicArrOutput,
                        UIHelpFunctionality.Arr2dToStringMatrix(basicArray2D, "   "));

                    return;
                }
            }

            CleanVisibleArrayCharacteristics();
            CleanBasicArrayField();
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

        private void CleanBasicArrayField()
        {
            PrintOutput(textBoxBasicArrOutput, null);
        }
    }
}
