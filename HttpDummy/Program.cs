using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TheBitBrine;

namespace HttpDummy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter server port: ");
            Port = int.Parse(Console.ReadLine());
            new Program().Run();
        }

        private QuickMan API;
        private static int Port = 80;
        public void Run()
        {
            API = new QuickMan();
            var Endpoints = new Dictionary<string, Action<HttpListenerContext>>();
            Endpoints.Add("*", Any);
            API.Start(Port, Endpoints, 20);
        }

        public void Any(HttpListenerContext Context)
        {
            Console.WriteLine($"Request sent to {Context.Request.Url}");
            var Body = new StreamReader(Context.Request.InputStream).ReadToEnd();
            Console.WriteLine($"Request body:\r\n{Body}");
            API.Respond("{\"ok\": true}", Context);
        }
    }
}
