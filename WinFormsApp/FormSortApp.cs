using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Internal;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFunctionality;

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

                    if (basicArray2D != null)
                    {
                        PrintOutput(textBoxBasicArrOutput,
                            UIHelpFunctionality.Arr2dToStringMatrix(basicArray2D, "\t"));
                    }

                }
            }
        }

        private void comboBoxDataTypeOfArr_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrType = UIHelpFunctionality.ChosenType(comboBoxDataTypeOfArr.SelectedItem.ToString());
        }

        private static void PrintOutput(TextBox textBoxField, string text)
        {
            textBoxField.Visible = true;
            textBoxField.Text = text;
        }
    }
}
