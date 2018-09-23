using System;
using System.Reflection;
using SharpyJson.Scripts.Core;
using SharpyJson.Scripts.Modules.Request;
using SharpyJson.Scripts.Modules.Settings;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace SharpyJson
{
    internal class Program
    {
        public class ClientService : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs e) {
                Console.WriteLine("Received: " + e.Data);

                Send(
                    RequestProcessor.Process(RawRequest.BuildFromString(e.Data)).Transform()
                );
            }
        }

        public static void Main(string[] args) {
            var appVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            
            Console.WriteLine("SharpyJson " + appVersion);
            
            var settings = SettingsManager.get();
            
            var websocketServer = new WebSocketServer ("ws://" + settings.GetHostName() + ":" + settings.GetPort());            
            
            websocketServer.AddWebSocketService<ClientService> ("/");
            
            websocketServer.Start ();
            Console.WriteLine("Server started");
            Console.WriteLine("Press any button to stop server...");
            Console.ReadKey (true);
            Console.WriteLine("Server stopped");
            websocketServer.Stop ();
        }
    }
}