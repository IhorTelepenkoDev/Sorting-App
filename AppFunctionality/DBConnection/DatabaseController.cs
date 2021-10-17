using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using AppFunctionality.Logging;

namespace AppFunctionality.DBConnection
{
    public class DatabaseController
    {
        public readonly bool isDatabaseConnectable = false;

        private readonly IDatabaseConnector dbConnector;
        public string tableName { get; private set; }

        private const string configFileName = "config.ini";

        private const string configSection = "database";
        private const string configParamServer = "server";
        private const string configParamDatabase = "database";
        private const string configParamUser = "user";
        private const string configParamPaaword = "password";

        private const string dbDefaultTableName = "Sortings";

        private readonly Logger log = Logger.GetInstance();

        public DatabaseController(string dbTableName = null)
        {
            if (dbTableName == null)
                tableName = dbDefaultTableName;
            else tableName = dbTableName;

            var configurationsPath = GetConfigFilePath();
            var configurationsReceiver = new ConfigReceiver(configurationsPath, configSection);

            dbConnector = new SQLServerConnector(configurationsReceiver.GetValue(configParamServer), configurationsReceiver.GetValue(configParamDatabase), 
                configurationsReceiver.GetValue(configParamUser), configurationsReceiver.GetValue(configParamPaaword));

            if (dbConnector.IsDatabaseConnectionPossible() == false)
            {
                isDatabaseConnectable = false;
                log.Error("The database cannot be connected");
                return;
            }
            else isDatabaseConnectable = true;

            if (dbConnector.DoesTableExist(tableName) == false)
            {
                log.Info($"Table '{tableName}' does not exist within the DB, it will be created");
                dbConnector.CreateSortTable(tableName);
            }
        }

        public DataTable GetSortHistory()
        {
            return dbConnector.ReadSortTable(tableName);
        }

        public void AddSortToHistory(string sorter, dynamic unsortedArr2d, dynamic sortedArr2d, DateTime currentDate)
        {
            log.Info($"New data of sorting '{sorter}' will be added");
            dbConnector.StoreSortData(tableName, sorter, ArrayHelpFunctionality.Arr2dToString(unsortedArr2d), 
                ArrayHelpFunctionality.Arr2dToString(sortedArr2d), currentDate.ToString("yyyy-MM-dd"));
        }

        public void CleanHistory()
        {
            log.Info("The history will be cleaned");
            dbConnector.CleanSortTable(tableName);
        }

        
        private string GetConfigFilePath()
        {
            string startupPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, configFileName);
            return startupPath;
        }
    }
}
