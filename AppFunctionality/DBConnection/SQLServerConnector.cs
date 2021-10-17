using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using AppFunctionality.Logging;

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

        private readonly Logger log = Logger.GetInstance();

        public SQLServerConnector(string serverName, string dbName, string userId, string password)
        {
            ServerName = serverName;
            DatabaseName = dbName;
            UserId = userId;
            Password = password;
        }

        public bool IsDatabaseConnectionPossible()
        {
            try
            {
                new SqlConnection(GetConnectionString());
                log.Debug("Database is successfully connected");
                return true;
            }
            catch (Exception e)
            {
                log.Debug($"Database cannot be connected: {e}");
                return false;
            }
        }

        public void CreateSortTable(string tableName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
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

                        log.Debug($"Table '{tableName}' is created successfully within the DB");
                    }
                }
            }
            catch (Exception e)
            {
                log.Error($"Failed to create a table '{tableName}' because: {e}");
            }
        }

        public void StoreSortData(string tableName, string sorterName, string unsortedArray, string sortedArray, string sortDate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand insertDataCommand = new SqlCommand())
                    {
                        insertDataCommand.Connection = connection;
                        insertDataCommand.CommandText =
                            $"INSERT INTO {tableName} ({sorterColumnName}, {unsortedArrColumnName}, {sortedArrColumnName}, {dateColumnName}) " +
                            $"VALUES ('{sorterName}', '{unsortedArray}', '{sortedArray}', '{sortDate}')";
                        try
                        {
                            insertDataCommand.ExecuteNonQuery();
                            log.Debug($"'{sorterName}' sorting data is successfully stored");
                        }
                        catch
                        {
                            log.Error($"Failed to insert the sorting data into DB table '{tableName}'");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error($"Failed to store the '{sorterName}' sorting data: {e}");
            }
        }

        public DataTable ReadSortTable(string tableName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    var selectQuery =
                        $"SELECT {sorterColumnName}, {unsortedArrColumnName}, {sortedArrColumnName}, {dateColumnName} FROM {tableName}";
                    string[] columnNames = new string[]
                        {sorterColumnName, unsortedArrColumnName, sortedArrColumnName, dateColumnName};
                    try
                    {
                        using (var adapter = new SqlDataAdapter(selectQuery, connection))
                        {
                            var sortDataTable = new DataTable();
                            adapter.Fill(sortDataTable);

                            if (sortDataTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < sortDataTable.Columns.Count; i++)
                                    sortDataTable.Columns[i].ColumnName = columnNames[i];

                                log.Debug($"The sort data is successfully read from DB table '{tableName}'");
                                return sortDataTable;
                            }
                        }
                    }
                    catch
                    {
                        log.Error($"Failed to process reading from DB table '{tableName}'");
                    }
                }
            }
            catch (Exception e)
            {
                log.Error($"Failed to read sort data from DB table '{tableName}': {e}");
            }

            return null;
        }

        public void CleanSortTable(string tableName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
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
                            log.Debug($"DB table '{tableName}' is successfully cleaned");
                        }
                        catch
                        {
                            log.Error($"Failed to process cleaning of DB table '{tableName}'");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error($"Failed to clean the DB table '{tableName}': {e}");
            }
        }

        private string GetConnectionString()
        {
            if(ServerName != null && DatabaseName != null && UserId != null && Password != null)
            {
                string connString = $"Server={ServerName};Database={DatabaseName};User Id={UserId};Password={Password};";
                return connString;
            }

            log.Warn("Not all DB connection parameters are received");
            return null;
        }

        public bool DoesTableExist(string tableName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand cleanDataCommand = new SqlCommand())
                    {
                        cleanDataCommand.Connection = connection;
                        cleanDataCommand.CommandText = "SELECT COUNT(*) " +
                                                       $"FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";
                        bool doesTableExist = Convert.ToBoolean(cleanDataCommand.ExecuteScalar());

                        log.Debug($"DB table '{tableName}' has existing status = {doesTableExist}");
                        return doesTableExist;
                    }
                }
            }
            catch (Exception e)
            {
                log.Error($"Failed to check if DB table '{tableName}' exists: {e}");
            }

            return false;
        }
    }
}
