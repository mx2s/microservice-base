using System.Collections.Generic;

namespace SharpyJson.Scripts.Modules.Websocket
{
    public class WebsocketRequests
    {
        private static int lastInsertedId = 0;
        private static readonly int valuesLimit = 2000;
        public static Dictionary<int, string> Responses;

        public static int GetNextKey() {
            lastInsertedId++;
            
            if (lastInsertedId >= valuesLimit) {
                lastInsertedId = 0;
            }
            
            if (Responses == null) {
                Responses = new Dictionary<int, string>();
            }
            return lastInsertedId;
        }
    }
}