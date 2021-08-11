﻿using System;
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
using AppFunctionality.ReceiveSorters;
using BaseSort;

namespace WinFormsApp
{
    public partial class FormSortApp : Form
    {
        public dynamic BasicArray2D { get; set; } = null;  // may be 2d array of different type
        public Type ArrayElemType { get; set; }
        public ISorter2D[] InstancesOfAvailableSortTypes { get; set; } = null;

        public FormSortApp()
        {
            InitializeComponent();
            SetBasicVisibleElements();
            //
        }

        private void buttonArrReadingByPath_Click(object sender, EventArgs e)
        {
            CleanVisibleArrayCharacteristics();

            CleanResultArrayField();

            BasicArray2D = null;

            var pathToFile = textBoxFilePath.Text;
            
            ArrayElemType = HelpFunctionalityOfArrReading.TypeOfArray2DStoredInFile(pathToFile);
            
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
            CleanResultArrayField();

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

        private void comboBoxSortingMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSortingMethod.SelectedIndex == comboBoxSortingMethod.Items.Count - 1)   //[select another folder of source]
            {
                comboBoxSortingMethod.SelectedIndex = 0;
                string folderPath = PathToFolderByBrowser();

                if (folderPath == null)
                    return;

                ReaderFromDLL folderReader = new ReaderFromDLL();
                InstancesOfAvailableSortTypes = folderReader.GetInstancesOfSpecificClassesInFolder<ISorter2D>(folderPath);

                if(InstancesOfAvailableSortTypes!=null)
                {
                    var namesOfSorts = (
                    from sortingInstance in InstancesOfAvailableSortTypes
                    select sortingInstance.SortName).ToArray();
                    SetItemsInDropDownOfAvailableSorts(namesOfSorts);
                }
                else SetItemsInDropDownOfAvailableSorts();
            }
        }

        private void buttonDoSort_Click(object sender, EventArgs e)
        {
            if(comboBoxSortingMethod.SelectedIndex > 0 && comboBoxSortingMethod.SelectedIndex < comboBoxSortingMethod.Items.Count - 1)
            {
                PerformSorting(comboBoxSortingMethod.SelectedIndex - 1);
            }

            if (comboBoxSortingMethod.SelectedIndex == 0)
                CleanResultArrayField();
        }

        private void SetItemsInDropDownOfDataTypesOfArr(string[] namesOfTypes = null)
        {
            comboBoxDataTypeOfArr.Items.Clear();
            comboBoxDataTypeOfArr.Items.Add("");
            if (namesOfTypes != null)
            foreach(var typeName in namesOfTypes)
            {
                comboBoxDataTypeOfArr.Items.Add(typeName);
            }
        }

        private void SetItemsInDropDownOfAvailableSorts(string[] namesOfSortings = null)
        {
            comboBoxSortingMethod.Items.Clear();
            comboBoxSortingMethod.Items.Add("");
            if (namesOfSortings != null)
            foreach (var sortName in namesOfSortings)
            {
                comboBoxSortingMethod.Items.Add(sortName);
            }
            comboBoxSortingMethod.Items.Add("[Select another source]");
        }

        private void SetBasicVisibleElements()
        {
            SetItemsInDropDownOfDataTypesOfArr(new string[] { "Integer", "Float", "Text" });
            SetItemsInDropDownOfAvailableSorts();
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

        private void CleanResultArrayField()
        {
            PrintOutput(textBoxResArrOutput, null);
        }

        private string PathToFolderByBrowser()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == DialogResult.OK)
                return folderBrowser.SelectedPath;
            else return null;
        }

        private void PerformSorting(int indexOfSortType)
        {
            try
            {
                var chosenSorter = InstancesOfAvailableSortTypes[indexOfSortType];

                dynamic sortingCopyOfBasic2dArr = UIHelpFunctionality.CopyOf2dArr(BasicArray2D, ArrayElemType);
                //PrintOutput(textBoxResArrOutput, UIHelpFunctionality.Arr2dToStringMatrix(sortingCopyOfBasic2dArr, "    "));

                
                chosenSorter.Sort(sortingCopyOfBasic2dArr);
                PrintOutput(textBoxResArrOutput, UIHelpFunctionality.Arr2dToStringMatrix(sortingCopyOfBasic2dArr, "    "));
            }
            catch { return; }
        }
    }
}
