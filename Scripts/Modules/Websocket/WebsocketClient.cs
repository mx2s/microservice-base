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

        public int SendMessage(string message) {
            ws.Send(message);

            int responseKey = WebsocketRequests.GetNextKey();

            WebsocketRequests.Responses[responseKey] = null;

            ws.OnMessage += (sender, e) => { WebsocketRequests.Responses[responseKey] = e.Data; };

            return responseKey;
        }
    }
}