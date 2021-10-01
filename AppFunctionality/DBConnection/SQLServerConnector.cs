using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AppFunctionality.DBConnection
{
    internal class SQLServerConnector: IDatabaseConnector
    {
        public string ServerName { private get; set; } = null;
        public string DatabaseName { private get; set; } = null;
        public string UserId { private get; set; } = null;
        public string Password { private get; set; } = null;

        private const string sorterColumnName = "Sorter";
        private const string unsortedArrColumnName = "Input_Array";
        private const string sortedArrColumnName = "Output_Array";
        private const string dateColumnName = "Date";

        public SQLServerConnector(string serverName, string dbName, string userId, string password)
        {
            ServerName = serverName;
            DatabaseName = dbName;
            UserId = userId;
            Password = password;
        }

        public void CreateSortTable(string tableName)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {   
                if(connection != null)
                {
                    connection.Open();
                    using (SqlCommand createTableCommand = new SqlCommand())
                    {
                        createTableCommand.Connection = connection;
                        createTableCommand.CommandText = $"CREATE TABLE {tableName} " +
                            "(Sorting_ID int IDENTITY(1,1) PRIMARY KEY, " +
                            $"{sorterColumnName} varchar(30), " +
                            $"{unsortedArrColumnName} varchar(max) NOT NULL, " +
                            $"{sortedArrColumnName} varchar(max), " +
                            $"{dateColumnName} date DEFAULT(CONVERT(date, getdate())) NOT NULL)";
                        createTableCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        public void StoreSortData(string tableName, string sorterName, string unsortedArray, string sortedArray, string sortDate)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                if (connection != null)
                {
                    connection.Open();
                    using (SqlCommand insertDataCommand = new SqlCommand())
                    {
                        insertDataCommand.Connection = connection;
                        insertDataCommand.CommandText = $"INSERT INTO {tableName} ({sorterColumnName}, {unsortedArrColumnName}, {sortedArrColumnName}, {dateColumnName}) " +
                            $"VALUES ('{sorterName}', '{unsortedArray}', '{sortedArray}', '{sortDate}')";
                        try
                        {
                            insertDataCommand.ExecuteNonQuery();
                        }
                        catch 
                        { 

                        }
                    }
                }
            }
        }

        public DataTable ReadSortTable(string tableName)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            { 
                if(connection != null)
                {
                    var selectQuery = $"SELECT {sorterColumnName}, {unsortedArrColumnName}, {sortedArrColumnName}, {dateColumnName} FROM {tableName}";
                    string[] columnNames = new string[] { sorterColumnName, unsortedArrColumnName, sortedArrColumnName, dateColumnName};
                    try
                    {
                        using (var adapter = new SqlDataAdapter(selectQuery, connection))
                        {
                            var sortDataTable = new DataTable();
                            adapter.Fill(sortDataTable);

                            if(sortDataTable != null)
                            {
                                for(int i = 0; i < sortDataTable.Columns.Count; i++)
                                    sortDataTable.Columns[i].ColumnName = columnNames[i];
                                
                                return sortDataTable;
                            }
                        }
                    }
                    catch { }
                }
            }

            return null;
        }

        public void CleanSortTable(string tableName)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                if (connection != null)
                {
                    connection.Open();
                    using (SqlCommand cleanDataCommand = new SqlCommand())
                    {
                        cleanDataCommand.Connection = connection;
                        cleanDataCommand.CommandText = $"DELETE FROM {tableName} " +
                            $"DBCC CHECKIDENT ('{tableName}', RESEED, 0)";
                        try
                        {
                            cleanDataCommand.ExecuteNonQuery();
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }

        private string GetConnectionString()
        {
            if(ServerName != null && DatabaseName != null && UserId != null && Password != null)
            {
                string connString = $"Server={ServerName};Database={DatabaseName};User Id={UserId};Password={Password};";
                return connString;
            }

            return null;
        }

        public bool DoesTableExist(string tableName)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                if (connection != null)
                {
                    connection.Open();
                    using (SqlCommand cleanDataCommand = new SqlCommand())
                    {
                        cleanDataCommand.Connection = connection;
                        cleanDataCommand.CommandText = "SELECT COUNT(*) " +
                            $"FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";

                        return Convert.ToBoolean(cleanDataCommand.ExecuteScalar());
                    }
                }
                return false;
            }
        }
    }
}
