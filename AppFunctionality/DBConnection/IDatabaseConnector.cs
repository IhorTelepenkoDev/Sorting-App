using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace AppFunctionality.DBConnection
{
    internal interface IDatabaseConnector
    {
        public void CreateSortTable(string tableName);
        public DataTable ReadSortTable(string tableName);
        public void StoreSortData(string tableName, string sorterName, string unsortedArray, string sortedArray, string date);
        public void CleanSortTable(string tableName);

        public bool DoesTableExist(string tableName);
        public bool IsDatabaseConnectionPossible();
    }
}
