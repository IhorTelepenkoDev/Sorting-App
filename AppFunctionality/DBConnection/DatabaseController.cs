using System;
using System.IO;
using System.Data;
using AppFunctionality.Logging;

namespace AppFunctionality.DBConnection
{
    public class DatabaseController
    {
        private readonly ILogger log;

        public readonly bool isDatabaseAvailable = false;

        private readonly IDatabaseConnector dbConnector;
        public string tableName { get; private set; }

        private const string ConfigFileName = "config.ini";

        private const string ConfigSection = "database";
        private const string ConfigParamServer = "server";
        private const string ConfigParamDatabase = "database";
        private const string ConfigParamUser = "user";
        private const string ConfigParamPassword = "password";

        private const string DbDefaultTableName = "Sortings";

        public DatabaseController(string dbTableName = null)
        {
            log = Logger.GetInstance();

            if (dbTableName == null)
                tableName = DbDefaultTableName;
            else tableName = dbTableName;

            var configurationsPath = GetConfigFilePath();
            var configurationsReceiver = new ConfigReader(configurationsPath, ConfigSection);

            dbConnector = new SqlServerConnector(configurationsReceiver.GetValue(ConfigParamServer), configurationsReceiver.GetValue(ConfigParamDatabase), 
                configurationsReceiver.GetValue(ConfigParamUser), configurationsReceiver.GetValue(ConfigParamPassword));

            if (dbConnector.IsDatabaseConnectionPossible() == false)
            {
                isDatabaseAvailable = false;
                log.Error("The database cannot be connected");
                return;
            }
            else isDatabaseAvailable = true;

            if (dbConnector.DoesTableExist(tableName) == false)
            {
                log.Info($"Table '{tableName}' does not exist within the DB, it will be created");
                dbConnector.CreateSortTable(tableName);
            }
        }

        public DataTable GetSortingHistory()
        {
            return dbConnector.ReadSortTable(tableName);
        }

        public void AddSortingToHistory(string sorter, dynamic unsortedArr2d, dynamic sortedArr2d, DateTime currentDate)
        {
            log.Info($"New data of sorting '{sorter}' will be added");
            dbConnector.StoreSortingData(tableName, sorter, ArrayHelpFunctionality.Arr2dToString(unsortedArr2d), 
                ArrayHelpFunctionality.Arr2dToString(sortedArr2d), currentDate.ToString("yyyy-MM-dd"));
        }

        public void CleanHistory()
        {
            log.Info("The history will be cleaned");
            dbConnector.CleanSortTable(tableName);
        }

        
        private string GetConfigFilePath()
        {
            string startupPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, ConfigFileName);
            return startupPath;
        }
    }
}
