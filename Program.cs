using System;
using System.Net;

namespace HttpWebServer
{
    internal class Program
    {
        private static string Path = @"C:\Users\X1009130";
        public static string SendResponse(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                Console.WriteLine("No client data was sent with the request.");
                return "KO";
            }
            System.IO.Stream body = request.InputStream;
            System.Text.Encoding encoding = request.ContentEncoding;
            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
            if (request.ContentType != null)
            {
                Console.WriteLine("Client data content type {0}", request.ContentType);
            }
            Console.WriteLine("Client data content length {0}", request.ContentLength64);

            Console.WriteLine("Start of client data:");
            // Convert the data to a string and display it on the console.
            string s = reader.ReadToEnd();
            try
            {
                System.IO.File.WriteAllText(System.IO.Path.Combine(Path, DateTime.Now.Ticks.ToString() + ".txt"), s, encoding);
            }
            catch(Exception ex)
            {

            }
            Console.WriteLine(s);
            Console.WriteLine("End of client data:");
            body.Close();
            reader.Close();

            return "OK";
        }

        private static void Main(string[] args)
        {
            if (args.Length >= 2)
            {
                Path = args[1];
                var ws = new HttpWebServer(SendResponse, args[0]);
                ws.Run();
                Console.WriteLine("A simple webserver. Press a key to quit.");
                Console.ReadKey();
                ws.Stop();
            }
            else
            {
                Console.WriteLine("First argument with url must be passed (ex : http://localhost:8080/test/).");
                Console.WriteLine("Second argument with path of writing payload must be passed (ex : C:\\tmp\\).");
                Console.ReadKey();
            }
        }
    }
}
