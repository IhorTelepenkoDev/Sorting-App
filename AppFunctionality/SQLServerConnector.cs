using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AppFunctionality
{
    public class SQLServerConnector
    {
        public string ServerName { private get; set; } = null;
        public string DatabaseName { private get; set; } = null;
        public string UserId { private get; set; } = null;
        public string Password { private get; set; } = null;

        public SQLServerConnector(string serverName, string dbName, string userId, string password)
        {
            ServerName = serverName;
            DatabaseName = dbName;
            UserId = userId;
            Password = password;
        }

        public void CreateStoredSortingsTable(string tableName)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using(SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = $"CREATE TABLE {tableName} " +
                        "(SortingID int NOT NULL IDENTITY CONSTRAINT AUTO_INCREMENT, " +
                        "Sorter varchar(25), " +
                        "InputArray varchar(max) NOT NULL, " +
                        "OutputArray varchar(max), " +
                        "Date datetime DEFAULT(CONVERT(date, getdate())) NOT NULL, " +
                        "PRIMARY KEY (SortingID))";
                    command.ExecuteNonQuery();
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
    }
}
