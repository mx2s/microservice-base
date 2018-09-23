using System.Data;
using Npgsql;
using SharpyJson.Scripts.Modules.Settings;

namespace SharpyJson.Scripts.Modules.DB
{
    public class DBConnector
    {
        private string connectionString;

        private readonly IDbConnection dbConnection;

        private static DBConnector instance;

        private DBConnector(string connectionString = "") {
            var settingsManager = SettingsManager.get();
            var dbConfig = settingsManager.DbConfig;
            
            this.connectionString =
                "Host=" + dbConfig["host"] + ";Username=" + dbConfig["login"] +
                ";Password=" + dbConfig["pass"] + ";Database=" + dbConfig["db"];
            
            dbConnection = new NpgsqlConnection(this.connectionString);
            dbConnection.Open();
        }
        
        public static DBConnector Get() {
            if (instance == null) {
                instance = new DBConnector();
            }
            return instance;
        }

        public IDbConnection GetDbConnection() {
            return dbConnection;
        }
    }
}