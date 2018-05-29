using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json.Linq;

namespace SharpyJson.Scripts.Modules.Settings
{
    public class SettingsManager
    {

        private static SettingsManager instance;

        public static SettingsManager get() {
            if (instance == null) {
                instance = new SettingsManager();
            }

            return instance;
        }

        protected string Environment;
        public Dictionary<string, string> DbConfig;

        private SettingsManager() {
            DbConfig = new Dictionary<string, string>();

            var streamReader = File.OpenText(@"config.json");

            Console.WriteLine(streamReader.ReadToEnd());
             
            var schema = JObject.Parse(streamReader.ReadToEnd());
            streamReader.Close();

            var nullCheckList = new ReadOnlyCollection<string>(new List<string>(new string[] {
                "env", "dev.db.host", "dev.db.port", "dev.db.login", "dev.db.pass", "dev.db.db"
            }));

            foreach (var checkToken in nullCheckList) {
                if (schema.SelectToken(checkToken) == null) {
                    Console.WriteLine("SettingsManager: config.json - token not found: " + checkToken);
                }
            }

            nullCheckList = null;

            // Setting env varables
            Environment = schema.SelectToken("env").Value<string>();

            var dbTokens = new ReadOnlyCollection<string>(new List<string>(new string[] {
                "host", "port", "login", "pass", "db"
            }));

            foreach (var dbToken in dbTokens) {
                DbConfig.Add(dbToken, schema.SelectToken(Environment + ".db." + dbToken).Value<string>());
            }

            dbTokens = null;

            Console.WriteLine(DbConfig["login"]);
        }
    }
}