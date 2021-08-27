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
using System.Text.RegularExpressions;
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

        private List<bool> isSortRunningOnTab;

        public SortApp()
        {
            InitializeComponent();
            SetBasicVisibleElements();

            isSortRunningOnTab = new List<bool>();
            isSortRunningOnTab.Add(false);  //the first default tab
            tabControlSortedArrResult.Selecting += new TabControlCancelEventHandler(tabControlSortedArrResult_SelectedTabChanged);
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

            PrintArr2dIntoGridView(BasicArray2D, dataGridViewUnsortedArr);
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

                    PrintArr2dIntoGridView(BasicArray2D, dataGridViewUnsortedArr);

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
                        foreach (var sortName in namesOfSorts)
                            CreateSortedArrTabPage(sortName);
                    }
                    else FillAvailableSortsDropDown();
                }

                comboBoxSelectedSorter.DroppedDown = true;
            }
            else
            {
                if(comboBoxSelectedSorter.SelectedIndex !=0)
                {
                    tabControlSortedArrResult.SelectedTab = tabControlSortedArrResult.TabPages[comboBoxSelectedSorter.SelectedIndex - 1];
                }

                if (isSortRunningOnTab[comboBoxSelectedSorter.SelectedIndex] == false)
                {
                    if (comboBoxSelectedSorter.SelectedIndex > 0)
                    {
                        if (dataGridViewUnsortedArr.RowCount != 0)
                            buttonDoSort.Enabled = true;
                    }
                    else buttonDoSort.Enabled = false;
                }
            }
                               

        }

        private void buttonDoSort_Click(object sender, EventArgs e)
        {
            if(comboBoxSelectedSorter.SelectedIndex > 0 && comboBoxSelectedSorter.SelectedIndex < comboBoxSelectedSorter.Items.Count - 1)
            {
                var selectedSorterIndex = comboBoxSelectedSorter.SelectedIndex - 1;
                Thread SortCaller = new Thread(new ThreadStart(() => PerformSorting(selectedSorterIndex)));
                SortCaller.Start();
            }

            if (comboBoxSelectedSorter.SelectedIndex == 0)
                CleanSortedArrayTextBox();
        }

        private void PerformSorting(int indexOfSortType)
        {
            var currentlySelectedTabIndex = tabControlSortedArrResult.SelectedIndex;
            var currentlySelectedSortedArrGridView = GetCurrentSortedArrGridView();
            try
            {
                var chosenSorter = InstancesOfAvailableSortTypes[indexOfSortType];
                var sortingCopyOfBasic2dArr = UIHelpFunctionality.Copy2dArr(BasicArray2D, ArrayElemType);

                MethodInvoker resultArrPrinter = new MethodInvoker(() => PrintArr2dIntoGridView(sortingCopyOfBasic2dArr, currentlySelectedSortedArrGridView));
                Invoke(resultArrPrinter);

                chosenSorter.MillisecTimeoutOnSortingDelay = trackBarSortSlower.Value;

                chosenSorter.FiredEventOnChangeOfArrayElements += VisualArrChangeWhenElementsSwapped(currentlySelectedSortedArrGridView);

                DisableBasicSortConfigControls();
                isSortRunningOnTab[currentlySelectedTabIndex] = true;

                chosenSorter.Sort(sortingCopyOfBasic2dArr);

                Invoke(resultArrPrinter);
            }
            catch { }

            if(tabControlSortedArrResult.SelectedIndex == currentlySelectedTabIndex)
                EnableBasicSortConfigControls();
            isSortRunningOnTab[currentlySelectedTabIndex] = false;
        }

        private void buttonAddSortTab_Click(object sender, EventArgs e)
        {
            /*TabPage newSortTab = new TabPage();
            newSortTab.Name = "tabPageSortedArr" + (tabControlSortedArrResult.TabPages.Count + 1).ToString();
            newSortTab.Text = "Sort " + (tabControlSortedArrResult.TabPages.Count + 1).ToString();

            CreateSortedArrGridView(newSortTab);

            tabControlSortedArrResult.TabPages.Add(newSortTab);

            isSortRunningOnTab.Add(false);*/
        }

        private void CreateSortedArrTabPage(string sortingName)
        {
            TabPage newSortTab = new TabPage();
            newSortTab.Name = "tabPageSortedArr" + (tabControlSortedArrResult.TabPages.Count).ToString() + sortingName;
            newSortTab.Text = sortingName;

            CreateSortedArrGridView(newSortTab, tabControlSortedArrResult.TabPages.Count - 1);

            tabControlSortedArrResult.TabPages.Add(newSortTab);

            isSortRunningOnTab.Add(false);
        }

        private void tabControlSortedArrResult_SelectedTabChanged(object sender, TabControlCancelEventArgs e)
        {            
            TabPage currentTab = (sender as TabControl).SelectedTab;
            int currentTabIndex = tabControlSortedArrResult.TabPages.IndexOf(currentTab);

            comboBoxArrDataType.SelectedIndex = currentTabIndex + 1;

            if (isSortRunningOnTab[currentTabIndex] == false)
                EnableBasicSortConfigControls();
            else
            {
                DisableBasicSortConfigControls();
            }

            // Validate the current page. To cancel the select, use:
            //e.Cancel = true;
        }

        private void PrintArr2dIntoGridView(dynamic array2d, DataGridView dataGridVewField)
        {
            try
            {
                dataGridVewField.RowCount = 0;

                int height = array2d.GetLength(0);
                int width = array2d.GetLength(1);

                dataGridVewField.ColumnCount = width;

                for (int rowIndex = 0; rowIndex < height; rowIndex++)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridVewField);

                    for (int columnIndex = 0; columnIndex < width; columnIndex++)
                    {
                        row.Cells[columnIndex].Value = array2d[rowIndex, columnIndex];
                    }

                    dataGridVewField.Rows.Add(row);
                }

                dataGridVewField.Rows[0].Selected = false;
            }
            catch { }
        }

        private ChangeOfArrayElementsHandler VisualArrChangeWhenElementsSwapped
            (DataGridView selectedSortedArrGridView)
        {
            ChangeOfArrayElementsHandler swappedElemsDisplay = (rowIndexSwappedElem1, colIndexSwappedElem1, rowIndexSwappedElem2, colIndexSwappedElem2) =>
            {
                CleanGridViewCellsBackColor(selectedSortedArrGridView);
                selectedSortedArrGridView[colIndexSwappedElem1, rowIndexSwappedElem1].Style.BackColor = Color.Beige;
                selectedSortedArrGridView[colIndexSwappedElem2, rowIndexSwappedElem2].Style.BackColor = Color.Beige;

                var tempCellVal = selectedSortedArrGridView[colIndexSwappedElem1, rowIndexSwappedElem1].Value;
                selectedSortedArrGridView[colIndexSwappedElem1, rowIndexSwappedElem1].Value = 
                    selectedSortedArrGridView[colIndexSwappedElem2, rowIndexSwappedElem2].Value;
                selectedSortedArrGridView[colIndexSwappedElem2, rowIndexSwappedElem2].Value = tempCellVal;
            };

            return swappedElemsDisplay;
        }

        private DataGridView GetCurrentSortedArrGridView()
        {
            var selectedTabPage = tabControlSortedArrResult.TabPages[tabControlSortedArrResult.SelectedIndex];

            DataGridView currentGridView = 
                selectedTabPage.Controls["dataGridViewSortedArr" + tabControlSortedArrResult.TabPages.IndexOf(selectedTabPage).ToString()] as DataGridView;

            return currentGridView;
        }

        private void CreateSortedArrGridView(TabPage selectedTabPage, int gridIndex)
        {
            var templateGridView = dataGridViewSortedArr0;
            var createdGridView = new DataGridView();

            //createdGridView.Name = "dataGridViewSortedArr" + (Convert.ToInt32(Regex.Match(selectedTabPage.Name, @"\d+$").Value) - 1).ToString();
            createdGridView.Name = "dataGridViewSortedArr" + gridIndex;
            createdGridView.Size = templateGridView.Size;
            createdGridView.Location = templateGridView.Location;
            createdGridView.BackgroundColor = templateGridView.BackgroundColor;
            createdGridView.CellBorderStyle = templateGridView.CellBorderStyle;
            createdGridView.ColumnHeadersVisible = templateGridView.ColumnHeadersVisible;
            createdGridView.RowHeadersVisible = templateGridView.RowHeadersVisible;
            createdGridView.RowTemplate = templateGridView.RowTemplate;
            createdGridView.ShowEditingIcon = templateGridView.ShowEditingIcon;
            createdGridView.AllowUserToAddRows = templateGridView.AllowUserToAddRows;
            createdGridView.AllowUserToDeleteRows = templateGridView.AllowUserToDeleteRows;
            createdGridView.AllowUserToResizeColumns = templateGridView.AllowUserToResizeColumns;
            createdGridView.AllowUserToResizeRows = templateGridView.AllowUserToResizeRows;
            createdGridView.ColumnHeadersHeight = templateGridView.ColumnHeadersHeight;
            createdGridView.ReadOnly = templateGridView.ReadOnly;
            createdGridView.TabIndex = templateGridView.TabIndex;
            createdGridView.AutoSizeColumnsMode = templateGridView.AutoSizeColumnsMode;

            selectedTabPage.Controls.Add(createdGridView);
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
            dataGridViewUnsortedArr.Rows.Clear();
        }

        private void CleanSortedArrayTextBox()
        {
            GetCurrentSortedArrGridView().Rows.Clear();
        }

        private void EnableBasicSortConfigControls()
        {
            //trackBarSortSlower.Enabled = true;
            buttonDoSort.Enabled = true;
            buttonRandomArrayAssign.Enabled = true;
            buttonReadArrByPath.Enabled = true;
            //comboBoxSelectedSorter.Enabled = true;
        }

        private void DisableBasicSortConfigControls()
        {
            //trackBarSortSlower.Enabled = false;
            buttonDoSort.Enabled = false;
            buttonRandomArrayAssign.Enabled = false;
            buttonReadArrByPath.Enabled = false;
            //comboBoxSelectedSorter.Enabled = false;
        }

        private void CleanGridViewCellsBackColor(DataGridView dataGridView)
        {
            for (var indexRow = 0; indexRow < dataGridView.Rows.Count; indexRow++)
            {
                for (var indexColumn = 0; indexColumn < dataGridView.Columns.Count; indexColumn++)
                {
                    if (dataGridView[indexColumn, indexRow].Style.BackColor != Color.Empty)
                        dataGridView[indexColumn, indexRow].Style.BackColor = Color.Empty;
                }
            }
        }
    }
}
