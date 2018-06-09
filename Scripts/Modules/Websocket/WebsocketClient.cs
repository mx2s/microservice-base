using System;
using SharpyJson.Scripts.Modules.Debug;
using WebSocketSharp;

namespace SharpyJson.Scripts.Modules.Websocket
{
    public class WebsocketClient
    {
        private WebSocket ws;
        public bool Connected;
        private string host;
        private int port;

        private int retries = 0;

        public WebsocketClient(string setHost, int setPort) {
            host = setHost;
            port = setPort;
            Reconnect();
        }

        public void Reconnect() {
            ws = new WebSocket("ws://" + host + ":" + port + "/");
            ws.Connect();
        }

        public void SendMessage(string message) {
            while (true) {
                if (retries >= 5) {
                    break;
                }
                try {
                    ws.Send(message);
                    retries = 0;
                    break;
                }
                catch (Exception e) {
                    DebugLog.get().Fatal("Cannot send message - ws " + host + ":" + port +
                                         " is closed! => retry: " + retries);
                    Reconnect();
                    retries++;
                }
            }
        }
    }
}