using System;
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
            var wssv = new WebSocketServer ("ws://localhost:9012");
            wssv.AddWebSocketService<Laputa> ("/");
            wssv.Start ();
            Console.WriteLine("Server started");
            Console.ReadKey (true);
            wssv.Stop ();
        }
    }
}