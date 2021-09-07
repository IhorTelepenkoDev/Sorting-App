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
        public List<dynamic> BasicArray2D { get; set; } = null;  // list of unsorted 2d arrays of any type
        public List<Type> ArrayElemType { get; set; }  //appropriate type of elements in unsorted basic array
        public ISorter2D[] InstancesOfAvailableSortTypes { get; set; } = null;

        private dynamic displayedBasicArr2D = null;
        private Type displayedArrElemType = null;

        private List<bool> isSortRunningOnTab;
        private DataGridView templateSortedArrGrid;

        public SortApp()
        {
            InitializeComponent();
            SetBasicVisibleElements();

            BasicArray2D = new List<dynamic>();
            BasicArray2D.Add(null);
            ArrayElemType = new List<Type>();
            ArrayElemType.Add(null);

            isSortRunningOnTab = new List<bool>();
            isSortRunningOnTab.Add(false);
            tabControlSortedArrResult.Selecting += new TabControlCancelEventHandler(tabControlSortedArrResult_SelectedTabChanged);

            templateSortedArrGrid = dataGridViewSortedArr0;
        }

        private void buttonReadArrByPath_Click(object sender, EventArgs e)
        {
            CleanVisibleArrayCharacteristics();

            CleanSortedArrayTextBox();

            var pathToFile = textBoxFilePath.Text;
            
            ArrayElemType[tabControlSortedArrResult.SelectedIndex] = 
                ArrReadingHelpFunctionality.GetTypeOfArray2DStoredInFile(pathToFile);
            
            if(ArrayElemType[tabControlSortedArrResult.SelectedIndex] != null)
            {
                var contentOfFile = ArrReadingHelpFunctionality.ReadFileContent(pathToFile);

                if (ArrayElemType[tabControlSortedArrResult.SelectedIndex] == typeof(int))
                    BasicArray2D[tabControlSortedArrResult.SelectedIndex] = new ArrayInitializer<int>(
                                contentOfFile, ArrReadingHelpFunctionality.Get2DArrayReaderFromDataSource<int>(pathToFile)).Array2D;
                if (ArrayElemType[tabControlSortedArrResult.SelectedIndex] == typeof(double))
                    BasicArray2D[tabControlSortedArrResult.SelectedIndex] = new ArrayInitializer<double>(
                                contentOfFile, ArrReadingHelpFunctionality.Get2DArrayReaderFromDataSource<double>(pathToFile)).Array2D;
                if (ArrayElemType[tabControlSortedArrResult.SelectedIndex] == typeof(string))
                    BasicArray2D[tabControlSortedArrResult.SelectedIndex] = new ArrayInitializer<string>(
                                contentOfFile, ArrReadingHelpFunctionality.Get2DArrayReaderFromDataSource<string>(pathToFile)).Array2D;

                displayedBasicArr2D = BasicArray2D[tabControlSortedArrResult.SelectedIndex];
                displayedArrElemType = ArrayElemType[tabControlSortedArrResult.SelectedIndex];
                for (int i = 0; i < BasicArray2D.Count; i++)
                    if (BasicArray2D[i] == null)
                    {
                        BasicArray2D[i] = displayedBasicArr2D;
                        ArrayElemType[i] = displayedArrElemType;
                    }
            }

            if (BasicArray2D[tabControlSortedArrResult.SelectedIndex] == null)
            {
                CleanUnsortedArrayTextBox();
                buttonDoSort.Enabled = false;
                return;
            }

            if(comboBoxSelectedSorter.SelectedIndex < comboBoxSelectedSorter.Items.Count - 1 && comboBoxSelectedSorter.Text != "")
                if(isSortRunningOnTab[tabControlSortedArrResult.SelectedIndex] == false)
                    buttonDoSort.Enabled = true;

            PrintArr2dIntoGridView(displayedBasicArr2D, dataGridViewUnsortedArr);
        }

        private void textBoxFilePath_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBoxFilePath.Text))
            {
                if (isSortRunningOnTab[tabControlSortedArrResult.SelectedIndex] == false)
                    buttonReadArrByPath.Enabled = true;
            }
            else buttonReadArrByPath.Enabled = false;
        }

        private void buttonRandomArrayAssign_Click(object sender, EventArgs e)
        {
            CleanSortedArrayTextBox();

            if (comboBoxArrDataType.SelectedIndex > 0)
            {
                if (numUpDownRowsInArr.Value > 0 && numUpDownColumnsInArr.Value > 0)
                {
                    ArrayElemType[tabControlSortedArrResult.SelectedIndex] =
                        UIHelpFunctionality.GetSelectedArrType(comboBoxArrDataType.SelectedItem.ToString());

                    int arr2dLengthRows = Convert.ToInt32(numUpDownRowsInArr.Value);
                    int arr2dLengthColumns = Convert.ToInt32(numUpDownColumnsInArr.Value);

                    if(ArrayElemType[tabControlSortedArrResult.SelectedIndex] == typeof(int))
                        BasicArray2D[tabControlSortedArrResult.SelectedIndex] = 
                            new ArrayInitializer<int>(arr2dLengthRows, arr2dLengthColumns).Array2D;
                    if (ArrayElemType[tabControlSortedArrResult.SelectedIndex] == typeof(double))
                        BasicArray2D[tabControlSortedArrResult.SelectedIndex] = 
                            new ArrayInitializer<double>(arr2dLengthRows, arr2dLengthColumns).Array2D;
                    if (ArrayElemType[tabControlSortedArrResult.SelectedIndex] == typeof(string))
                        BasicArray2D[tabControlSortedArrResult.SelectedIndex] = 
                            new ArrayInitializer<string>(arr2dLengthRows, arr2dLengthColumns).Array2D;

                    displayedBasicArr2D = BasicArray2D[tabControlSortedArrResult.SelectedIndex];
                    displayedArrElemType = ArrayElemType[tabControlSortedArrResult.SelectedIndex];
                    for (int i = 0; i < BasicArray2D.Count; i++)
                        if (BasicArray2D[i] == null)
                        {
                            BasicArray2D[i] = displayedBasicArr2D;
                            ArrayElemType[i] = displayedArrElemType;
                        }

                    PrintArr2dIntoGridView(displayedBasicArr2D, dataGridViewUnsortedArr);

                    if (comboBoxSelectedSorter.SelectedIndex < comboBoxSelectedSorter.Items.Count - 1 && comboBoxSelectedSorter.Text != "")
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
                if (numUpDownRowsInArr.Value > 0 && numUpDownColumnsInArr.Value > 0)
                    if (isSortRunningOnTab[tabControlSortedArrResult.SelectedIndex] == false)
                        buttonRandomArrayAssign.Enabled = true;

            } else buttonRandomArrayAssign.Enabled = false;

        }

        private void numUpDownRowsInArr_ValueChanged(object sender, EventArgs e)
        {
            if(numUpDownRowsInArr.Value == 0)
                buttonRandomArrayAssign.Enabled = false;
            else
                if (numUpDownColumnsInArr.Value > 0 && comboBoxArrDataType.SelectedIndex > 0)
                    if (isSortRunningOnTab[tabControlSortedArrResult.SelectedIndex] == false)
                        buttonRandomArrayAssign.Enabled = true;
        }
        
        private void numUpDownColumnsInArr_ValueChanged(object sender, EventArgs e)
        {
            if (numUpDownColumnsInArr.Value == 0)
                buttonRandomArrayAssign.Enabled = false;
            else
                if (numUpDownRowsInArr.Value > 0 && comboBoxArrDataType.SelectedIndex > 0)
                    if (isSortRunningOnTab[tabControlSortedArrResult.SelectedIndex] == false)
                        buttonRandomArrayAssign.Enabled = true;
        }

        private void comboBoxSelectedSorter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSelectedSorter.SelectedIndex == comboBoxSelectedSorter.Items.Count - 1)   //[select another folder of source]
            {
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

                        isSortRunningOnTab.Clear();
                        BasicArray2D.Clear();
                        ArrayElemType.Clear();

                        FillAvailableSortsDropDown(namesOfSorts);
                        tabControlSortedArrResult.TabPages.Clear();
                        foreach (var sortName in namesOfSorts)
                        {
                            BasicArray2D.Add(displayedBasicArr2D);
                            ArrayElemType.Add(displayedArrElemType);
                            CreateSortedArrTabPage(sortName);
                        }
                    }
                    else FillAvailableSortsDropDown();
                }

                comboBoxSelectedSorter.SelectedIndex = 0;
                comboBoxSelectedSorter.DroppedDown = true;
            }
            else
            {                
                tabControlSortedArrResult.SelectedTab = tabControlSortedArrResult.TabPages[comboBoxSelectedSorter.SelectedIndex];

                if (BasicArray2D.Count != 0)
                {
                    displayedBasicArr2D = BasicArray2D[tabControlSortedArrResult.SelectedIndex];
                    displayedArrElemType = ArrayElemType[comboBoxSelectedSorter.SelectedIndex];
                    PrintArr2dIntoGridView(displayedBasicArr2D, dataGridViewUnsortedArr);
                }
                
                if(isSortRunningOnTab.Count != 0)
                    if (isSortRunningOnTab[comboBoxSelectedSorter.SelectedIndex] == false)
                    {
                        if (BasicArray2D[tabControlSortedArrResult.SelectedIndex] != null)
                             buttonDoSort.Enabled = true;
                        
                        else buttonDoSort.Enabled = false;
                    }             
            }                    

        }

        private void buttonDoSort_Click(object sender, EventArgs e)
        {
            var selectedSorterIndex = comboBoxSelectedSorter.SelectedIndex;
            var currentlySelectedSortedArrGridView = GetCurrentSortedArrGridView();
            Thread SortCaller = new Thread(new ThreadStart(() => PerformSorting(selectedSorterIndex, currentlySelectedSortedArrGridView)));
            SortCaller.Start();
        }

        private void PerformSorting(int indexOfSortType, DataGridView selectedSortedArrGridView)
        {
            var currentlySelectedTabIndex = indexOfSortType;
            //var currentlySelectedSortedArrGridView = GetCurrentSortedArrGridView();
            //var currentlySelectedSortedArrGridView = this.Controls["dataGridViewSortedArr" + currentlySelectedTabIndex.ToString()] as DataGridView;
            try
            {
                var chosenSorter = InstancesOfAvailableSortTypes[indexOfSortType];
                var sortingCopyOfBasic2dArr = 
                    UIHelpFunctionality.Copy2dArr(BasicArray2D[currentlySelectedTabIndex], ArrayElemType[currentlySelectedTabIndex]);

                MethodInvoker resultArrPrinter =  new MethodInvoker(() => PrintArr2dIntoGridView(sortingCopyOfBasic2dArr, selectedSortedArrGridView));
                Invoke(resultArrPrinter);

                chosenSorter.MillisecTimeoutOnSortingDelay = trackBarSortSlower.Value;

                chosenSorter.FiredEventOnChangeOfArrayElements += VisualArrChangeWhenElementsSwapped(selectedSortedArrGridView);

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

        private void CreateSortedArrTabPage(string sortingName)
        {
            TabPage newSortTab = new TabPage();
            newSortTab.Name = "tabPageSortedArr" + (tabControlSortedArrResult.TabPages.Count).ToString() + sortingName;
            newSortTab.Text = sortingName;

            CreateSortedArrGridView(newSortTab, tabControlSortedArrResult.TabPages.Count);

            tabControlSortedArrResult.TabPages.Add(newSortTab);

            isSortRunningOnTab.Add(false);
        }

        private void tabControlSortedArrResult_SelectedTabChanged(object sender, TabControlCancelEventArgs e)
        {            
            if(tabControlSortedArrResult.TabPages.Count != 0 && BasicArray2D.Count != 0)
            {
                TabPage currentTab = (sender as TabControl).SelectedTab;
                int currentTabIndex = tabControlSortedArrResult.TabPages.IndexOf(currentTab);

                comboBoxSelectedSorter.SelectedIndex = currentTabIndex;
                displayedBasicArr2D = BasicArray2D[comboBoxSelectedSorter.SelectedIndex];
                displayedArrElemType = ArrayElemType[comboBoxSelectedSorter.SelectedIndex];
                PrintArr2dIntoGridView(displayedBasicArr2D, dataGridViewUnsortedArr);

                if (isSortRunningOnTab[currentTabIndex] == false)
                    EnableBasicSortConfigControls();
                else
                {
                    DisableBasicSortConfigControls();
                }
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
            var createdGridView = new DataGridView();

            //createdGridView.Name = "dataGridViewSortedArr" + (Convert.ToInt32(Regex.Match(selectedTabPage.Name, @"\d+$").Value) - 1).ToString();
            createdGridView.Name = "dataGridViewSortedArr" + gridIndex;
            createdGridView.Size = templateSortedArrGrid.Size;
            createdGridView.Location = templateSortedArrGrid.Location;
            createdGridView.BackgroundColor = templateSortedArrGrid.BackgroundColor;
            createdGridView.CellBorderStyle = templateSortedArrGrid.CellBorderStyle;
            createdGridView.ColumnHeadersVisible = templateSortedArrGrid.ColumnHeadersVisible;
            createdGridView.RowHeadersVisible = templateSortedArrGrid.RowHeadersVisible;
            createdGridView.RowTemplate = templateSortedArrGrid.RowTemplate;
            createdGridView.ShowEditingIcon = templateSortedArrGrid.ShowEditingIcon;
            createdGridView.AllowUserToAddRows = templateSortedArrGrid.AllowUserToAddRows;
            createdGridView.AllowUserToDeleteRows = templateSortedArrGrid.AllowUserToDeleteRows;
            createdGridView.AllowUserToResizeColumns = templateSortedArrGrid.AllowUserToResizeColumns;
            createdGridView.AllowUserToResizeRows = templateSortedArrGrid.AllowUserToResizeRows;
            createdGridView.ColumnHeadersHeight = templateSortedArrGrid.ColumnHeadersHeight;
            createdGridView.ReadOnly = templateSortedArrGrid.ReadOnly;
            createdGridView.TabIndex = templateSortedArrGrid.TabIndex;
            createdGridView.AutoSizeColumnsMode = templateSortedArrGrid.AutoSizeColumnsMode;

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
            //comboBoxSelectedSorter.Items.Add("");
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
            if (comboBoxSelectedSorter.SelectedIndex < comboBoxSelectedSorter.Items.Count - 1 && comboBoxSelectedSorter.Text != "" 
                                                                                            && dataGridViewUnsortedArr.RowCount != 0)
                buttonDoSort.Enabled = true;
            if (comboBoxArrDataType.SelectedIndex > 0 && numUpDownRowsInArr.Value > 0 && numUpDownColumnsInArr.Value > 0)
                buttonRandomArrayAssign.Enabled = true;
            if (File.Exists(textBoxFilePath.Text))
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
