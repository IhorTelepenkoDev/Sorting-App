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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<dynamic> BasicArray2D { get; set; } = null;  // list of unsorted 2d arrays of any type
        public List<Type> ArrayElemType { get; set; }  //appropriate type of elements in unsorted basic array
        public ISorter2D[] InstancesOfAvailableSortTypes { get; set; } = null;

        private Thread[] runningSortThreads = null;

        private dynamic displayedBasicArr2D = null;
        private Type displayedArrElemType = null;

        private List<bool> isSortRunningOnTab;
        private DataGridView templateSortedArrGrid;

        private Dictionary<TabPage, Color> sortedArrTabColors;
        private Color notFinishedSortingTabColor = Color.MistyRose;
        private Color finishedSortingTabColor = Color.FromArgb(150, 229, 187);

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

            sortedArrTabColors = new Dictionary<TabPage, Color>();
            sortedArrTabColors.Add(tabControlSortedArrResult.SelectedTab, Color.White);

            tabControlSortedArrResult.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControlSortedArrResult.DrawItem += new DrawItemEventHandler(tabControlSortedArrResult_DrawItem);

            log.Info("The application is started and set");
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
                log.Debug("Array in file has type: " + ArrayElemType[tabControlSortedArrResult.SelectedIndex].ToString());

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

                log.Info("Unsorted array is read from file");

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
                buttonStartSorting.Enabled = false;
                return;
            }

            if(comboBoxSelectedSorter.SelectedIndex < comboBoxSelectedSorter.Items.Count - 1 && comboBoxSelectedSorter.Text != "")
                if(isSortRunningOnTab[tabControlSortedArrResult.SelectedIndex] == false)
                    buttonStartSorting.Enabled = true;

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

                    log.Info("Unsorted array is randomly generated");

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
                        buttonStartSorting.Enabled = true;

                    return;
                }
            }

            buttonStartSorting.Enabled = false;

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
                log.Info("Selecting of a new source of sorters");

                string folderPath = null;
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                    folderPath = folderBrowser.SelectedPath;

                if (folderPath != null)
                {
                    ReaderFromDLL folderReader = new ReaderFromDLL();
                    InstancesOfAvailableSortTypes = folderReader.GetInstancesOfSortersInFolder(folderPath);
                    log.Debug("Quantity of newly uploaded sorters: " + InstancesOfAvailableSortTypes.Length);

                    if(runningSortThreads != null)
                        foreach (var thread in runningSortThreads)
                                if(thread != null)
                                    if (thread.IsAlive)
                                        thread.Abort();

                    if (InstancesOfAvailableSortTypes != null)
                        runningSortThreads = new Thread[0];
                    else
                        runningSortThreads = new Thread[InstancesOfAvailableSortTypes.Length];

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

                        log.Info("New sorters are uploaded");
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
                             buttonStartSorting.Enabled = true;
                        
                        else buttonStartSorting.Enabled = false;
                    }             
            }                    

        }

        private void buttonStartSorting_Click(object sender, EventArgs e)
        {
            var selectedSorterIndex = comboBoxSelectedSorter.SelectedIndex;
            var currentlySelectedSortedArrGridView = GetCurrentSortedArrGridView();

            log.Debug($"Sorting will start for sorter #{selectedSorterIndex} ('{InstancesOfAvailableSortTypes[selectedSorterIndex].SortName}')");

            var sortingCopyOfBasic2dArr =
                    UIHelpFunctionality.Copy2dArr(BasicArray2D[selectedSorterIndex], ArrayElemType[selectedSorterIndex]);

            MethodInvoker resultArrPrinter = new MethodInvoker(() => PrintArr2dIntoGridView(sortingCopyOfBasic2dArr, currentlySelectedSortedArrGridView));

            InstancesOfAvailableSortTypes[selectedSorterIndex].MillisecTimeoutOnSortingDelay = trackBarSortSlower.Value;
            InstancesOfAvailableSortTypes[selectedSorterIndex].FiredEventOnChangeOfArrayElements += 
                VisualArrChangeWhenElementsSwapped(currentlySelectedSortedArrGridView);
            InstancesOfAvailableSortTypes[selectedSorterIndex].FiredEventOnSortingEnd += new SortingEndHandler(() =>
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    Invoke(resultArrPrinter);   //the array is already sorted, is displayed
                    SetTabHeaderColor(tabControlSortedArrResult.TabPages[selectedSorterIndex], finishedSortingTabColor);
                    if (tabControlSortedArrResult.SelectedIndex == selectedSorterIndex)
                        EnableBasicSortConfigControls();
                    isSortRunningOnTab[selectedSorterIndex] = false;

                    log.Info($"Sorting '{InstancesOfAvailableSortTypes[selectedSorterIndex].SortName}' is finished");
                }));
            });

            Invoke(resultArrPrinter); // the array is not sorted yet, is displayed
            Thread SortCaller = new Thread(new ThreadStart(() => PerformSorting(selectedSorterIndex, sortingCopyOfBasic2dArr)));
            runningSortThreads[selectedSorterIndex] = SortCaller;
            DisableBasicSortConfigControls();

            log.Info("Sorting is about to be started");

            SortCaller.IsBackground = true;
            SortCaller.Start();
        }

        private void trackBarSortSlower_Scroll(object sender, EventArgs e)
        {
            if (InstancesOfAvailableSortTypes != null)
            {
                int currentlySelectedSortIndex = comboBoxSelectedSorter.SelectedIndex;
                InstancesOfAvailableSortTypes[currentlySelectedSortIndex].MillisecTimeoutOnSortingDelay = trackBarSortSlower.Value;
                log.Info($"Sorter '{InstancesOfAvailableSortTypes[currentlySelectedSortIndex].SortName}' got an updated slower value");
            }
        }

        private void PerformSorting(int indexOfSortType, dynamic unsortedArray2d)
        {
            var selectedSorter = InstancesOfAvailableSortTypes[indexOfSortType];
            log.Debug($"New thread is opened for sorter #{indexOfSortType} ('{selectedSorter.SortName}')");

            isSortRunningOnTab[indexOfSortType] = true;
            SetTabHeaderColor(tabControlSortedArrResult.TabPages[indexOfSortType], notFinishedSortingTabColor);

            log.Info($"Sorting '{selectedSorter.SortName} starts'");
            selectedSorter.Sort(unsortedArray2d);
        }

        private void CreateSortedArrTabPage(string sortingName)
        {
            TabPage newSortTab = new TabPage();
            newSortTab.Name = "tabPageSortedArr" + (tabControlSortedArrResult.TabPages.Count).ToString() + sortingName;
            newSortTab.Text = sortingName;

            CreateSortedArrGridView(newSortTab, tabControlSortedArrResult.TabPages.Count);

            sortedArrTabColors.Add(newSortTab, Color.White);
            tabControlSortedArrResult.TabPages.Add(newSortTab);

            isSortRunningOnTab.Add(false);
        }

        private void tabControlSortedArrResult_SelectedTabChanged(object sender, TabControlCancelEventArgs e)
        {
            if (tabControlSortedArrResult.TabPages.Count != 0 && BasicArray2D.Count != 0)
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
                    trackBarSortSlower.Value = InstancesOfAvailableSortTypes[currentTabIndex].MillisecTimeoutOnSortingDelay;
                    DisableBasicSortConfigControls();
                }
            } else
            {
                if (tabControlSortedArrResult.TabPages.Count == 0)
                    log.Warn("Selected sorted array tab is changed whereas no tabs created");
                if(BasicArray2D.Count == 0)
                    log.Warn("Selected sorted array tab is changed whereas no arrays available");
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
            catch (Exception e)
            {
                log.Debug($"Printing of array into grid '{dataGridVewField.Name}' failed due to exception {e}");
            }
        }

        private ChangeOfArrayElementsHandler VisualArrChangeWhenElementsSwapped
            (DataGridView selectedSortedArrGridView)
        {
            ChangeOfArrayElementsHandler swappedElemsDisplay = (rowIndexSwappedElem1, colIndexSwappedElem1, rowIndexSwappedElem2, colIndexSwappedElem2) =>
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    CleanGridViewCellsBackColor(selectedSortedArrGridView);
                    selectedSortedArrGridView[colIndexSwappedElem1, rowIndexSwappedElem1].Style.BackColor = Color.Beige;
                    selectedSortedArrGridView[colIndexSwappedElem2, rowIndexSwappedElem2].Style.BackColor = Color.Beige;

                    var tempCellVal = selectedSortedArrGridView[colIndexSwappedElem1, rowIndexSwappedElem1].Value;
                    selectedSortedArrGridView[colIndexSwappedElem1, rowIndexSwappedElem1].Value =
                        selectedSortedArrGridView[colIndexSwappedElem2, rowIndexSwappedElem2].Value;
                    selectedSortedArrGridView[colIndexSwappedElem2, rowIndexSwappedElem2].Value = tempCellVal;
                }));
            };

            return swappedElemsDisplay;
        }

        private DataGridView GetCurrentSortedArrGridView()
        {
            var selectedTabPage = tabControlSortedArrResult.TabPages[tabControlSortedArrResult.SelectedIndex];

            DataGridView currentGridView = 
                selectedTabPage.Controls["dataGridViewSortedArr" + tabControlSortedArrResult.TabPages.IndexOf(selectedTabPage).ToString()] as DataGridView;

            if (currentGridView == null)
                log.Error($"No grid was found for sorted array displaying (selected sorted array tab is #{tabControlSortedArrResult.SelectedIndex})");

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

        private void SetTabHeaderColor(TabPage tab, Color color)
        {
            sortedArrTabColors[tab] = color;
            tabControlSortedArrResult.Invalidate();
        }
        private void tabControlSortedArrResult_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            using (Brush br = new SolidBrush(sortedArrTabColors[tabControlSortedArrResult.TabPages[e.Index]]))
            {
                e.Graphics.FillRectangle(br, e.Bounds);
                SizeF sz = e.Graphics.MeasureString(tabControlSortedArrResult.TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(tabControlSortedArrResult.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 
                    (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                Rectangle rect = e.Bounds;
                rect.Offset(0, 1);
                rect.Inflate(0, -1);
                e.Graphics.DrawRectangle(Pens.White, rect);
                e.DrawFocusRectangle();
            }
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
                buttonStartSorting.Enabled = true;
            if (comboBoxArrDataType.SelectedIndex > 0 && numUpDownRowsInArr.Value > 0 && numUpDownColumnsInArr.Value > 0)
                buttonRandomArrayAssign.Enabled = true;
            if (File.Exists(textBoxFilePath.Text))
                buttonReadArrByPath.Enabled = true;
            //comboBoxSelectedSorter.Enabled = true;
        }

        private void DisableBasicSortConfigControls()
        {
            //trackBarSortSlower.Enabled = false;
            buttonStartSorting.Enabled = false;
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
