using Microsoft.Owin.Hosting;
using System;

namespace CSharpRestBackend
{
    class Program
    {
        static void Main(string[] args)
        {
            const string url = "http://localhost:15002";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("REST API en {0}", url);
                Console.ReadLine();
            }
        }
    }
}
