using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace AppFunctionality.DBConnection
{
    public class DatabaseController
    {
        private readonly IDatabaseConnector dbConnector;
        public string tableName { get; private set; }

        private const string configFileName = "config.ini";

        private const string configSection = "database";
        private const string configParamServer = "server";
        private const string configParamDatabase = "database";
        private const string configParamUser = "user";
        private const string configParamPaaword = "password";

        private const string dbDefaultTableName = "Sortings";

        public DatabaseController(string dbTableName = null)
        {
            if (dbTableName == null)
                tableName = dbDefaultTableName;
            else tableName = dbTableName;

            var configurationsPath = GetConfigFilePath();
            var configurationsReceiver = new ConfigReceiver(configurationsPath, configSection);

            dbConnector = new SQLServerConnector(configurationsReceiver.GetValue(configParamServer), configurationsReceiver.GetValue(configParamDatabase), 
                configurationsReceiver.GetValue(configParamUser), configurationsReceiver.GetValue(configParamPaaword));

            if (dbConnector.DoesTableExist(tableName) == false)
                dbConnector.CreateSortTable(tableName);
        }

        public DataTable GetSortHistory()
        {
            return dbConnector.ReadSortTable(tableName);
        }

        public void AddSortToHistory(string sorter, dynamic unsortedArr2d, dynamic sortedArr2d, DateTime currentDate)
        {
            dbConnector.StoreSortData(tableName, sorter, ArrayHelpFunctionality.Arr2dToString(unsortedArr2d), 
                ArrayHelpFunctionality.Arr2dToString(sortedArr2d), currentDate.ToString("yyyy-MM-dd"));
        }

        public void CleanHistory()
        {
            dbConnector.CleanSortTable(tableName);
        }

        
        private string GetConfigFilePath()
        {
            string startupPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, configFileName);
            return startupPath;
        }
    }
}
