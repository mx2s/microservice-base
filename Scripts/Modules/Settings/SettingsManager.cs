using System;
using System.Collections.Generic;
using System.Configuration;

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

        public string configAbsolutePath = null;
        
        protected string Environment;
        public Dictionary<string, string> DbConfig;

        private SettingsManager() {
            DbConfig = new Dictionary<string, string>();
            
            DbConfig.Add("host", ConfigurationManager.AppSettings["db_host"]);
            DbConfig.Add("login", ConfigurationManager.AppSettings["db_login"]);
            DbConfig.Add("pass", ConfigurationManager.AppSettings["db_pass"]);
            DbConfig.Add("db", ConfigurationManager.AppSettings["db_name"]);
        }
    }
}