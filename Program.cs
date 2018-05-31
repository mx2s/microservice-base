using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using SharpyJson.Scripts.Core;
using SharpyJson.Scripts.Models;
using SharpyJson.Scripts.Modules.Processor;
using SharpyJson.Scripts.Modules.Settings;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace SharpyJson
{
    internal class Program
    {
        public class Laputa : WebSocketBehavior
        {
            protected override void OnMessage (MessageEventArgs e)
            {
                Console.WriteLine("Received: " + e.Data);
                var msg = e.Data == "PINGPONG"
                    ? "Ok"
                    : "Not ok";

                Send(msg);
            }
        }
        
        public static void Main(string[] args) {

            // TODO: remove dummy json
            string json = @"{
                'Token': 'daf12f12',
                'RequestType': 2,
                'Data': {
                    'Login': 'admin',
                    'Pass': '1234'
                }
            }";
            var settingsManager = SettingsManager.get();
            
            var newRequest = RequestBuilder.Build(json);

            var processor = RequestProcessor.get();
            processor.process(newRequest);

            User usr = User.Find(2);
            Console.WriteLine(usr.Password);

//            User.Create(new User() {
//                Login = "testuser",
//                Password = "1234"
//            });

            //Console.WriteLine(newRequest.Token);

            /*
            var wssv = new WebSocketServer ("ws://localhost:9012");
            wssv.AddWebSocketService<Laputa> ("/");
            wssv.Start ();
            Console.WriteLine("Server started");
            Console.ReadKey (true);
            wssv.Stop ();
            */
        }
    }
}