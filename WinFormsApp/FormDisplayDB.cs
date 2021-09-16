using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFunctionality;

namespace WinFormsApp
{
    public partial class FormDisplayDB : Form
    {
        private SortApp parentSortAppForm;
        private SQLServerConnector historyDatabaseConnector;
        private string dbTable;
        public FormDisplayDB(SortApp parentForm, SQLServerConnector dbConnect, string dbTableName)
        {
            InitializeComponent();
            parentSortAppForm = parentForm;
            historyDatabaseConnector = dbConnect;
            dbTable = dbTableName;

            DisplayDatabaseContent(historyDatabaseConnector, dbTable);
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

            DisplayDatabaseContent(historyDatabaseConnector, dbTable);
        }

        private void buttonCleanContent_Click(object sender, EventArgs e)
        {
            historyDatabaseConnector.CleanStoredSortData(dbTable);
            DisplayDatabaseContent(historyDatabaseConnector, dbTable);
        }

        private void DisplayDatabaseContent(SQLServerConnector dbConnector, string tableName)
        {
            var contentDataTable = dbConnector.GetStoredSortData(tableName);
            dataGridViewSortHistory.DataSource = contentDataTable;

            dataGridViewSortHistory.ClearSelection();
        }

        private void FormDisplayDB_FormClosing(object sender, FormClosingEventArgs e)
        {
            Button parentHistoryButton = parentSortAppForm.Controls["buttonSortingHistory"] as Button;
            parentHistoryButton.Enabled = true;
        }

    }
}
