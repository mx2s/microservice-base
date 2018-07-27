using System;

namespace SharpyJson.Scripts.Modules.Debug
{
    public class DebugLog
    {
        private LogLevels logLevel = LogLevels.None;
        
        private static DebugLog instance;
        
        private DebugLog() {}
        
        public static DebugLog get() {
            if (instance == null) {
                instance = new DebugLog();
            }

            return instance;
        }
        
        public void SetLogLevel(LogLevels level) {
            logLevel = level;
        }
        
        public void Message(string message) {
            if (logLevel <= LogLevels.Message) {
                Console.WriteLine("Message: " + message);
            }
        }
        
        public void Log(string message) {
            if (logLevel <= LogLevels.Log) {
                Console.WriteLine("Log: " + message);
            }
        }
        
        public void Warning(string message) {
            if (logLevel <= LogLevels.Warning) {
                Console.WriteLine("Warning: " + message);
            }
        }
        
        public void Error(string message) {
            if (logLevel <= LogLevels.Error) {
                Console.WriteLine("Error: " + message);
            }
        }
        
        public void Fatal(string message) {
            if (logLevel <= LogLevels.Fatal) {
                Console.WriteLine("Fatal: " + message);
            }
        }
    }
}