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
using System.Threading;
using System.Windows.Forms;
using AppFunctionality;
using AppFunctionality.ReceiveArrayFromFile;
using AppFunctionality.ReceiveSorters;
using BaseSort;

namespace WinFormsApp
{
    public partial class SortApp : Form
    {
        public dynamic BasicArray2D { get; set; } = null;  // may be 2d array of different type
        public Type ArrayElemType { get; set; }
        public ISorter2D[] InstancesOfAvailableSortTypes { get; set; } = null;

        public SortApp()
        {
            InitializeComponent();
            SetBasicVisibleElements();
            //
        }

        private void buttonReadArrByPath_Click(object sender, EventArgs e)
        {
            CleanVisibleArrayCharacteristics();

            CleanSortedArrayTextBox();

            BasicArray2D = null;

            var pathToFile = textBoxFilePath.Text;
            
            ArrayElemType = ArrReadingHelpFunctionality.GetTypeOfArray2DStoredInFile(pathToFile);
            
            if(ArrayElemType != null)
            {
                var contentOfFile = ArrReadingHelpFunctionality.ReadFileContent(pathToFile);

                if (ArrayElemType == typeof(int))
                    BasicArray2D = new ArrayInitializer<int>(
                                contentOfFile, ArrReadingHelpFunctionality.Get2DArrayReaderFromDataSource<int>(pathToFile)).Array2D;
                if (ArrayElemType == typeof(double))
                    BasicArray2D = new ArrayInitializer<double>(
                                contentOfFile, ArrReadingHelpFunctionality.Get2DArrayReaderFromDataSource<double>(pathToFile)).Array2D;
                if (ArrayElemType == typeof(string))
                    BasicArray2D = new ArrayInitializer<string>(
                                contentOfFile, ArrReadingHelpFunctionality.Get2DArrayReaderFromDataSource<string>(pathToFile)).Array2D;
            }

            if (BasicArray2D == null)
            {
                CleanUnsortedArrayTextBox();
                buttonDoSort.Enabled = false;
                return;
            }

            if(comboBoxSelectedSorter.SelectedIndex > 0)
                buttonDoSort.Enabled = true;

            textBoxUnsortedArr.Text =
                UIHelpFunctionality.Arr2dToString(BasicArray2D, "    ");
        }

        private void textBoxFilePath_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBoxFilePath.Text))
                buttonReadArrByPath.Enabled = true;
            else buttonReadArrByPath.Enabled = false;
        }

        private void buttonRandomArrayAssign_Click(object sender, EventArgs e)
        {
            CleanSortedArrayTextBox();

            BasicArray2D = null;

            if (comboBoxArrDataType.SelectedIndex > 0)
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

                    textBoxUnsortedArr.Text =
                        UIHelpFunctionality.Arr2dToString(BasicArray2D, "   ");

                    if (comboBoxSelectedSorter.SelectedIndex > 0)
                        buttonDoSort.Enabled = true;

                    return;
                }
            }

            buttonDoSort.Enabled = false;

            CleanVisibleArrayCharacteristics();
            CleanUnsortedArrayTextBox();
        }

        private void comboBoxArrDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxArrDataType.SelectedIndex != 0)
            {
                ArrayElemType = UIHelpFunctionality.GetSelectedArrType(comboBoxArrDataType.SelectedItem.ToString());

                if (numUpDownRowsInArr.Value > 0 && numUpDownColumnsInArr.Value > 0)
                    buttonRandomArrayAssign.Enabled = true;

            } else buttonRandomArrayAssign.Enabled = false;

        }

        private void numUpDownRowsInArr_ValueChanged(object sender, EventArgs e)
        {
            if(numUpDownRowsInArr.Value == 0)
                buttonRandomArrayAssign.Enabled = false;
            else
                if (numUpDownColumnsInArr.Value > 0 && comboBoxArrDataType.SelectedIndex > 0)
                    buttonRandomArrayAssign.Enabled = true;
        }
        
        private void numUpDownColumnsInArr_ValueChanged(object sender, EventArgs e)
        {
            if (numUpDownColumnsInArr.Value == 0)
                buttonRandomArrayAssign.Enabled = false;
            else
                if (numUpDownRowsInArr.Value > 0 && comboBoxArrDataType.SelectedIndex > 0)
                buttonRandomArrayAssign.Enabled = true;
        }

        private void comboBoxSelectedSorter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSelectedSorter.SelectedIndex == comboBoxSelectedSorter.Items.Count - 1)   //[select another folder of source]
            {
                comboBoxSelectedSorter.SelectedIndex = 0;

                string folderPath = null;
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                    folderPath = folderBrowser.SelectedPath;

                if (folderPath != null)
                {
                    ReaderFromDLL folderReader = new ReaderFromDLL();
                    InstancesOfAvailableSortTypes = folderReader.GetInstancesOfSortersInFolder(folderPath);

                    if (InstancesOfAvailableSortTypes != null)
                    {
                        var namesOfSorts = (from sortingInstance in InstancesOfAvailableSortTypes
                                            select sortingInstance.SortName).ToArray();

                        FillAvailableSortsDropDown(namesOfSorts);
                    }
                    else FillAvailableSortsDropDown();
                }

                comboBoxSelectedSorter.DroppedDown = true;
            }

            if (comboBoxSelectedSorter.SelectedIndex > 0)
            {
                if (textBoxUnsortedArr.Text != "")
                    buttonDoSort.Enabled = true;
            }
            else buttonDoSort.Enabled = false;
        }

        private void buttonDoSort_Click(object sender, EventArgs e)
        {
            if(comboBoxSelectedSorter.SelectedIndex > 0 && comboBoxSelectedSorter.SelectedIndex < comboBoxSelectedSorter.Items.Count - 1)
            {
                Thread SortCaller = new Thread(new ThreadStart(() => PerformSorting(comboBoxSelectedSorter.SelectedIndex - 1)));
                SortCaller.Start();
                //PerformSorting(comboBoxSelectedSorter.SelectedIndex - 1);    //index of received Sorter - as parameter
            }

            if (comboBoxSelectedSorter.SelectedIndex == 0)
                CleanSortedArrayTextBox();
        }

        private void FillArrDataTypesDropDown(string[] namesOfTypes = null)
        {
            comboBoxArrDataType.Items.Clear();
            comboBoxArrDataType.Items.Add("");
            if (namesOfTypes != null)
            foreach(var typeName in namesOfTypes)
            {
                comboBoxArrDataType.Items.Add(typeName);
            }
        }

        private void FillAvailableSortsDropDown(string[] namesOfSortings = null)
        {
            comboBoxSelectedSorter.Items.Clear();
            comboBoxSelectedSorter.Items.Add("");
            if (namesOfSortings != null)
            foreach (var sortName in namesOfSortings)
            {
                comboBoxSelectedSorter.Items.Add(sortName);
            }
            comboBoxSelectedSorter.Items.Add("[Select another source]");
        }

        private void SetBasicVisibleElements()
        {
            string[] defaultDataTypesAvailable = { "Integer", 
                                                   "Float", 
                                                   "Text" };

            FillArrDataTypesDropDown(defaultDataTypesAvailable);
            FillAvailableSortsDropDown();
        }

        private void CleanVisibleArrayCharacteristics()
        {
            comboBoxArrDataType.SelectedIndex = 0;
            numUpDownRowsInArr.Value = 0;
            numUpDownColumnsInArr.Value = 0;
        }

        private void CleanUnsortedArrayTextBox()
        {
            textBoxUnsortedArr.Text = null;
        }

        private void CleanSortedArrayTextBox()
        {
            textBoxSortedArr.Text = null;
        }

        private void PerformSorting(int indexOfSortType)
        {
            try
            {
                var chosenSorter = InstancesOfAvailableSortTypes[indexOfSortType];

                var sortingCopyOfBasic2dArr = UIHelpFunctionality.Copy2dArr(BasicArray2D, ArrayElemType);
                textBoxSortedArr.Text = UIHelpFunctionality.Arr2dToString(sortingCopyOfBasic2dArr, "    ");

                chosenSorter.MillisecTimeoutOnSortingDelay = trackBarSortSlower.Value;
                chosenSorter.FiredActionOnChangeOfElementsInArray = () => textBoxSortedArr.Text = UIHelpFunctionality.Arr2dToString(sortingCopyOfBasic2dArr, "    ");


                trackBarSortSlower.Enabled = false;
                buttonDoSort.Enabled = false;
                buttonRandomArrayAssign.Enabled = false;
                buttonReadArrByPath.Enabled = false;
                comboBoxSelectedSorter.Enabled = false;

                chosenSorter.Sort(sortingCopyOfBasic2dArr);
                textBoxSortedArr.Text = UIHelpFunctionality.Arr2dToString(sortingCopyOfBasic2dArr, "    ");

            }
            catch { }

            trackBarSortSlower.Enabled = true;
            buttonDoSort.Enabled = true;
            buttonRandomArrayAssign.Enabled = true;
            buttonReadArrByPath.Enabled = true;
            comboBoxSelectedSorter.Enabled = true;
        }
    }
}
