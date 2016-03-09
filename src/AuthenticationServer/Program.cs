using Microsoft.Owin.Hosting;
using System;

namespace AuthenticationServer
{
    class Program
    {
        static void Main(string[] args)
        {
            const string url = "http://localhost:15001";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Servidor de autenticación en {0}", url);
                Console.ReadLine();
            }
        }
    }
}
