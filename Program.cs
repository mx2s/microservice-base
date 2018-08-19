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
                var msg = e.Data == "PINGPONG"
                    ? "Ok"
                    : "Not ok";

                Send(
                    RequestProcessor.Process(RawRequest.BuildFromString(e.Data)).Transform()
                );
            }
        }

        public static void Main(string[] args) {
            var appVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            
            Console.WriteLine("SharpyJson " + appVersion);
            
            var settings = SettingsManager.get();
            
            var wssv = new WebSocketServer ("ws://" + settings.GetHostName() + ":" + settings.GetPort());            
            
            wssv.AddWebSocketService<ClientService> ("/");
            
            wssv.Start ();
            Console.WriteLine("Server started");
            Console.WriteLine("Press any button to stop server...");
            Console.ReadKey (true);
            Console.WriteLine("Server stopped");
            wssv.Stop ();
        }
    }
}