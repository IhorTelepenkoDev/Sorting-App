using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms;
using System.ComponentModel;
using AppFunctionality.DBConnection;
using AppFunctionality.Logging;
using BaseSort;

namespace WinFormSortApp
{
    public partial class SortApp
    {
        private readonly Logger log = Logger.GetInstance();

        public List<dynamic> BasicArray2D { get; set; } = null;  // list of unsorted 2d arrays of any type
        public List<Type> ArrayElemType { get; set; }  //appropriate type of elements in unsorted basic array
        public ISorter2D[] InstancesOfAvailableSortTypes { get; set; } = null;

        private DatabaseController dbController;

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

            dbController = new DatabaseController();

            BasicArray2D = new List<dynamic> {null};
            ArrayElemType = new List<Type> {null};

            isSortRunningOnTab = new List<bool> {false};
            tabControlSortedArrResult.Selecting += new TabControlCancelEventHandler(tabControlSortedArrResult_SelectedTabChanged);

            templateSortedArrGrid = dataGridViewSortedArr0;

            sortedArrTabColors = new Dictionary<TabPage, Color> {{tabControlSortedArrResult.SelectedTab, Color.White}};

            tabControlSortedArrResult.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControlSortedArrResult.DrawItem += new DrawItemEventHandler(tabControlSortedArrResult_DrawItem);

            components = new Container();
            toolTipValue = new ToolTip(components);

            log.Info("The application is started and set");
        }

        private void SetNewUnsortedArrayAsDefault(int selectedSorterIndex)
        {
            displayedBasicArr2D = BasicArray2D[selectedSorterIndex];
            displayedArrElemType = ArrayElemType[selectedSorterIndex];
            for (int i = 0; i < BasicArray2D.Count; i++)
                if (BasicArray2D[i] == null)
                {
                    BasicArray2D[i] = displayedBasicArr2D;
                    ArrayElemType[i] = displayedArrElemType;
                }
        }

        private void PerformSorting(int indexOfSortType, dynamic unsortedArray2d)
        {
            var selectedSorter = InstancesOfAvailableSortTypes[indexOfSortType];
            log.Debug($"New thread is opened for sorter #{indexOfSortType} ('{selectedSorter.SortName}')");

            isSortRunningOnTab[indexOfSortType] = true;
            SetTabHeaderColor(tabControlSortedArrResult.TabPages[indexOfSortType], notFinishedSortingTabColor);

            log.Info($"Sorting '{selectedSorter.SortName}' starts");
            selectedSorter.Sort(unsortedArray2d);
        }

        private void CreateSortedArrTabPage(string sortingName)
        {
            TabPage newSortTab = new TabPage
            {
                Name = "tabPageSortedArr" + (tabControlSortedArrResult.TabPages.Count) + sortingName,
                Text = sortingName
            };

            CreateSortedArrGridView(newSortTab, tabControlSortedArrResult.TabPages.Count);

            sortedArrTabColors.Add(newSortTab, Color.White);
            tabControlSortedArrResult.TabPages.Add(newSortTab);

            isSortRunningOnTab.Add(false);
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
                if (!(e is NullReferenceException))
                    log.Debug($"Printing of array into grid '{dataGridVewField.Name}' failed due to exception {e}");
            }
        }

        private ChangeOfArrayElementsHandler VisualArrChangeWhenElementsSwapped
            (DataGridView selectedSortedArrGridView)
        {
            ChangeOfArrayElementsHandler swappedElemsDisplay = (rowIndexSwappedElem1, colIndexSwappedElem1, rowIndexSwappedElem2, colIndexSwappedElem2) =>
            {
                Invoke(new MethodInvoker(delegate
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
            var createdGridView = new DataGridView
            {
                Name = "dataGridViewSortedArr" + gridIndex,
                Size = templateSortedArrGrid.Size,
                Location = templateSortedArrGrid.Location,
                BackgroundColor = templateSortedArrGrid.BackgroundColor,
                CellBorderStyle = templateSortedArrGrid.CellBorderStyle,
                ColumnHeadersVisible = templateSortedArrGrid.ColumnHeadersVisible,
                RowHeadersVisible = templateSortedArrGrid.RowHeadersVisible,
                RowTemplate = templateSortedArrGrid.RowTemplate,
                ShowEditingIcon = templateSortedArrGrid.ShowEditingIcon,
                AllowUserToAddRows = templateSortedArrGrid.AllowUserToAddRows,
                AllowUserToDeleteRows = templateSortedArrGrid.AllowUserToDeleteRows,
                AllowUserToResizeColumns = templateSortedArrGrid.AllowUserToResizeColumns,
                AllowUserToResizeRows = templateSortedArrGrid.AllowUserToResizeRows,
                ColumnHeadersHeight = templateSortedArrGrid.ColumnHeadersHeight,
                ReadOnly = templateSortedArrGrid.ReadOnly,
                TabIndex = templateSortedArrGrid.TabIndex,
                AutoSizeColumnsMode = templateSortedArrGrid.AutoSizeColumnsMode
            };


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
                foreach (var typeName in namesOfTypes)
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
