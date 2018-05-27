using System;
using Newtonsoft.Json;
using SharpyJson.Scripts.Core;
using SharpyJson.Scripts.Modules.Processor;
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
            
            string json = @"{
                'Token': 'daf12f12',
                'RequestType': 2,
                'Data': {
                    'Login': 'admin',
                    'Pass': '1234'
                }
            }";
            
            var newRequest = RequestBuilder.Build(json);

            var processor = RequestProcessor.get();
            processor.process(newRequest);

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