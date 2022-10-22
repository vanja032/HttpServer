using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace DataHttpServerProject.DataHttpServerDocs
{
    public delegate byte[] ProcessDataDelegate(string data);
    public class DataHttpServer
    {
        private const int ThreadHandler = 2;
        private readonly ProcessDataDelegate handler;
        private readonly HttpListener listener;
        public DataHttpServer(HttpListener listener, string url, ProcessDataDelegate handler)
        {
            this.listener = listener;
            this.handler = handler;
            listener.Prefixes.Add(url);
        }
        public void StartServer()
        {
            if (listener.IsListening)
                return;
            listener.Start();
            for(int i = 0; i < ThreadHandler; i++)
            {
                listener.GetContextAsync().ContinueWith(ProcessRequestHandler);
            }
            Console.WriteLine("Check");
        }
        private void ProcessRequestHandler(Task<HttpListenerContext> result)
        {
            var context = result.Result;
            if (!listener.IsListening)
                return;
            listener.GetContextAsync().ContinueWith(ProcessRequestHandler);
            string request = new StreamReader(context.Request.InputStream).ReadToEnd();

            var response = handler.Invoke(request);
            context.Response.ContentLength64 = response.Length;
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            var output = context.Response.OutputStream;
            output.WriteAsync(response, 0, response.Length);
            output.Close();
        }
    }
}
