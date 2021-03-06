using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AppFunctionality;
using AppFunctionality.ReceiveArrayFromFile;
using AppFunctionality.ReceiveSorters;
using AppFunctionality.StoreSortingToFIle;
using BaseSort;

namespace WinFormSortApp
{
    public partial class SortApp : Form
    {

        private void textBoxFilePath_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBoxFilePath.Text))
            {
                if (isSortRunningOnTab[tabControlSortedArrResult.SelectedIndex] == false)
                    buttonReadArrByPath.Enabled = true;
            }
            else buttonReadArrByPath.Enabled = false;
        }

        private void buttonSelectArrFileLocation_Click(object sender, EventArgs e)
        {
            string filePath = null;
            OpenFileDialog fileBrowser = new OpenFileDialog();
            if (fileBrowser.ShowDialog() == DialogResult.OK)
                filePath = fileBrowser.FileName;

            textBoxFilePath.Text = filePath;
        }

        private void buttonReadArrByPath_Click(object sender, EventArgs e)
        {
            CleanVisibleArrayCharacteristics();

            CleanSortedArrayTextBox();

            var pathToFile = textBoxFilePath.Text;

            ArrayElemType[tabControlSortedArrResult.SelectedIndex] =
                ArrReadingHelpFunctionality.GetTypeOfArray2DStoredInFile(pathToFile);

            if (ArrayElemType[tabControlSortedArrResult.SelectedIndex] != null)
            {
                log.Debug("Array in file has type: " + ArrayElemType[tabControlSortedArrResult.SelectedIndex].ToString());

                var contentOfFile = ArrReadingHelpFunctionality.ReadFileContent(pathToFile);

                log.Info("Unsorted array will be read from file");

                if (ArrayElemType[tabControlSortedArrResult.SelectedIndex] == typeof(int))
                    BasicArray2D[tabControlSortedArrResult.SelectedIndex] = new ArrayInitializer<int>(
                                contentOfFile, ArrReadingHelpFunctionality.Get2DArrayReaderFromDataSource<int>(pathToFile)).Array2D;
                if (ArrayElemType[tabControlSortedArrResult.SelectedIndex] == typeof(double))
                    BasicArray2D[tabControlSortedArrResult.SelectedIndex] = new ArrayInitializer<double>(
                                contentOfFile, ArrReadingHelpFunctionality.Get2DArrayReaderFromDataSource<double>(pathToFile)).Array2D;
                if (ArrayElemType[tabControlSortedArrResult.SelectedIndex] == typeof(string))
                    BasicArray2D[tabControlSortedArrResult.SelectedIndex] = new ArrayInitializer<string>(
                                contentOfFile, ArrReadingHelpFunctionality.Get2DArrayReaderFromDataSource<string>(pathToFile)).Array2D;

                SetNewUnsortedArrayAsDefault(tabControlSortedArrResult.SelectedIndex);
                SetTabHeaderColor(tabControlSortedArrResult.SelectedTab, Color.White);
            }
            else
            {
                CleanUnsortedArrayTextBox();
                buttonStartSorting.Enabled = false;
                return;
            }

            if (comboBoxSelectedSorter.SelectedIndex < comboBoxSelectedSorter.Items.Count - 1 && comboBoxSelectedSorter.Text != "")
                if (isSortRunningOnTab[tabControlSortedArrResult.SelectedIndex] == false)
                    buttonStartSorting.Enabled = true;

            PrintArr2dIntoGridView(displayedBasicArr2D, dataGridViewUnsortedArr);
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

        private void buttonRandomArrayAssign_Click(object sender, EventArgs e)
        {
            CleanSortedArrayTextBox();

            if (comboBoxArrDataType.SelectedIndex > 0)
            {
                if (numUpDownRowsInArr.Value > 0 && numUpDownColumnsInArr.Value > 0)
                {
                    ArrayElemType[tabControlSortedArrResult.SelectedIndex] =
                        ArrayHelpFunctionality.GetSelectedArrType(comboBoxArrDataType.SelectedItem.ToString());

                    int arr2dLengthRows = Convert.ToInt32(numUpDownRowsInArr.Value);
                    int arr2dLengthColumns = Convert.ToInt32(numUpDownColumnsInArr.Value);

                    log.Info("Unsorted array will be randomly generated");

                    if (ArrayElemType[tabControlSortedArrResult.SelectedIndex] == typeof(int))
                        BasicArray2D[tabControlSortedArrResult.SelectedIndex] =
                            new ArrayInitializer<int>(arr2dLengthRows, arr2dLengthColumns).Array2D;
                    if (ArrayElemType[tabControlSortedArrResult.SelectedIndex] == typeof(double))
                        BasicArray2D[tabControlSortedArrResult.SelectedIndex] =
                            new ArrayInitializer<double>(arr2dLengthRows, arr2dLengthColumns).Array2D;
                    if (ArrayElemType[tabControlSortedArrResult.SelectedIndex] == typeof(string))
                        BasicArray2D[tabControlSortedArrResult.SelectedIndex] =
                            new ArrayInitializer<string>(arr2dLengthRows, arr2dLengthColumns).Array2D;

                    SetNewUnsortedArrayAsDefault(tabControlSortedArrResult.SelectedIndex);
                    SetTabHeaderColor(tabControlSortedArrResult.SelectedTab, Color.White);

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
                    DLLSortersReader folderReader = new DLLSortersReader();
                    InstancesOfAvailableSortTypes = folderReader.GetInstancesOfSortersInFolder(folderPath);
                    log.Debug("Quantity of newly uploaded sorters: " + InstancesOfAvailableSortTypes.Length);

                    if(runningSortThreads != null)
                        foreach (var thread in runningSortThreads)
                                if(thread != null)
                                    if (thread.IsAlive)
                                        thread.Abort();

                    runningSortThreads = InstancesOfAvailableSortTypes == null ? new Thread[0] : 
                        new Thread[InstancesOfAvailableSortTypes.Length];

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
                        buttonStartSorting.Enabled = BasicArray2D[tabControlSortedArrResult.SelectedIndex] != null;
                    }             
            }                    

        }

        private void buttonStartSorting_Click(object sender, EventArgs e)
        {
            var selectedSorterIndex = comboBoxSelectedSorter.SelectedIndex;
            var currentlySelectedSortedArrGridView = GetCurrentSortedArrGridView();

            log.Debug($"Sorting will start for sorter #{selectedSorterIndex} ('{InstancesOfAvailableSortTypes[selectedSorterIndex].SortName}')");

            var sortingCopyOfBasic2dArr =
                    ArrayHelpFunctionality.Copy2dArr(BasicArray2D[selectedSorterIndex], ArrayElemType[selectedSorterIndex]);

            MethodInvoker resultArrPrinter = new MethodInvoker(() => PrintArr2dIntoGridView(sortingCopyOfBasic2dArr, currentlySelectedSortedArrGridView));


            InstancesOfAvailableSortTypes[selectedSorterIndex].MillisecTimeoutOnSortingDelay = GetSortSlowerValueMilliseconds();

            InstancesOfAvailableSortTypes[selectedSorterIndex].CleanEventChangeOfArrElements();
            InstancesOfAvailableSortTypes[selectedSorterIndex].FiredEventOnChangeOfArrayElements += 
                VisualArrChangeWhenElementsSwapped(currentlySelectedSortedArrGridView);

            InstancesOfAvailableSortTypes[selectedSorterIndex].CleanEventSortingEnd();
            InstancesOfAvailableSortTypes[selectedSorterIndex].FiredEventOnSortingEnd += new SortingEndHandler(() =>
            {
                Invoke(new MethodInvoker(delegate
                {
                    Invoke(resultArrPrinter);   //the array is already sorted, is displayed

                    if(dbController.isDatabaseAvailable)
                        dbController.AddSortingToHistory(InstancesOfAvailableSortTypes[selectedSorterIndex].SortName, 
                            BasicArray2D[selectedSorterIndex], sortingCopyOfBasic2dArr, DateTime.Today);
                    else
                    {
                        log.Debug("History database cannot be used, sorting data will be stored to file");
                        var fileHistoryStorer = new SortingWriterToJSONFile();
                        fileHistoryStorer.StoreSortingData(InstancesOfAvailableSortTypes[selectedSorterIndex].SortName, 
                            BasicArray2D[selectedSorterIndex], sortingCopyOfBasic2dArr, DateTime.Today);
                    }

                    SetTabHeaderColor(tabControlSortedArrResult.TabPages[selectedSorterIndex], finishedSortingTabColor);
                    if (tabControlSortedArrResult.SelectedIndex == selectedSorterIndex)
                        EnableBasicSortConfigControls();
                    isSortRunningOnTab[selectedSorterIndex] = false;

                    log.Info($"Sorting '{InstancesOfAvailableSortTypes[selectedSorterIndex].SortName}' is finished");

                }));
            });

            Invoke(resultArrPrinter); // the array is not sorted yet, is displayed
            Thread sortCaller = new Thread(new ThreadStart(() => PerformSorting(selectedSorterIndex, sortingCopyOfBasic2dArr)));
            runningSortThreads[selectedSorterIndex] = sortCaller;
            DisableBasicSortConfigControls();

            log.Info("Sorting is about to be started");

            sortCaller.IsBackground = true;
            sortCaller.Start();
        }

        private void trackBarSortSpeed_Scroll(object sender, EventArgs e)
        {
            if (InstancesOfAvailableSortTypes != null)
            {
                var currentlySelectedSortIndex = comboBoxSelectedSorter.SelectedIndex;
                InstancesOfAvailableSortTypes[currentlySelectedSortIndex].MillisecTimeoutOnSortingDelay = GetSortSlowerValueMilliseconds();
            }
            //toolTipValue.SetToolTip(trackBarSortSlower, (trackBarSortSlower.Value) + " %");
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
                    trackBarSortSpeed.Value = GetTrackBarSortSpeedValue(InstancesOfAvailableSortTypes[currentTabIndex]
                        .MillisecTimeoutOnSortingDelay);
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

        private void buttonSortingHistory_Click(object sender, EventArgs e)
        {
            var historyDisplayForm = new FormDisplayHistory(this, dbController);
            
            if (historyDisplayForm.isSortingHistoryAvailable)
            {
                historyDisplayForm.Show();
                buttonSortingHistory.Enabled = false;
            }
            else
            {
                string messageTitle = "Previous History";
                string messageText = "No sorting history found.";
                MessageBox.Show(messageText, messageTitle);
            }
        }

    }
}
