using System;
using System.Diagnostics;
using System.Net;
using DataHttpServerProject.DataHttpServerDocs;

namespace DataHttpServerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServer();
            Console.ReadKey();
        }
        private static void StartServer()
        {
            var HttpListener = new HttpListener();
            var DataHttpServer = new DataHttpServer(HttpListener, "http://+:1234/test/", ProcessResponse);
            DataHttpServer.StartServer();
        }
        private static byte[] ProcessResponse(string data)
        {
            Console.WriteLine(data);
            return new byte[0];
        }
    }
}
