using System.Data;

namespace AppFunctionality.DBConnection
{
    internal interface IDatabaseConnector
    {
        public void CreateSortTable(string tableName);
        public DataTable ReadSortTable(string tableName);
        public void StoreSortingData(string tableName, string sorterName, string unsortedArray, string sortedArray, string date);
        public void CleanSortTable(string tableName);

        public bool DoesTableExist(string tableName);
        public bool IsDatabaseConnectionPossible();
    }
}
