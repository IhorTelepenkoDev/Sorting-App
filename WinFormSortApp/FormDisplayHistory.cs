using System;
using System.Data;
using System.Windows.Forms;
using AppFunctionality.DBConnection;
using AppFunctionality.Logging;

namespace WinFormSortApp
{
    public partial class FormDisplayHistory : Form
    {
        private readonly ILogger log;

        private SortApp parentSortAppForm;
        private DatabaseController historyDatabaseController;

        public FormDisplayHistory(SortApp parentForm, DatabaseController dbController)
        {
            log = Logger.GetInstance();

            InitializeComponent();
            parentSortAppForm = parentForm;
            historyDatabaseController = dbController;

            DisplayDatabaseContent();
            log.Info("History of sortings is opened");
        }

        private void FormDisplayDB_Load(object sender, EventArgs e)
        {
            dataGridViewSortHistory.ClearSelection();
        }

        private void buttonUpdateContent_Click(object sender, EventArgs e)
        {
            dataGridViewSortHistory.Columns.Clear();
            dataGridViewSortHistory.Refresh();
            int visualReloadingTimeoutMs = 75;
            System.Threading.Thread.Sleep(visualReloadingTimeoutMs);

            DisplayDatabaseContent();
            log.Info("Data of sortings history is updated");
        }

        /*private void buttonCleanContent_Click(object sender, EventArgs e)
        {
            historyDatabaseController.CleanHistory();
            log.Info("Data of sortings history is cleaned");

            DisplayDatabaseContent();
        }*/

        private void DisplayDatabaseContent()
        {
            DataTable contentDataTable = historyDatabaseController.GetSortingHistory();
            dataGridViewSortHistory.DataSource = contentDataTable;
            dataGridViewSortHistory.FirstDisplayedScrollingRowIndex = 
                dataGridViewSortHistory.RowCount - 1;   // to display the last
            dataGridViewSortHistory.ClearSelection();
        }

        private void FormDisplayDB_FormClosing(object sender, FormClosingEventArgs e)
        {
            const string openSortHistoryButtonName = "buttonSortingHistory";

            try
            {
                Button parentHistoryButton = parentSortAppForm.Controls[openSortHistoryButtonName] as Button;
                parentHistoryButton.Enabled = true;
            }
            catch (NullReferenceException)
            {
                log.Fatal("'history of sortings' button is not found. The name do not match.");
            }
        }

    }
}
