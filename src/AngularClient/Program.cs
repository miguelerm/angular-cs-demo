using Microsoft.Owin.Hosting;
using System;
using System.Diagnostics;

namespace AngularClient
{
    class Program
    {
        static void Main(string[] args)
        {
            const string url = "http://localhost:15003";
            using (WebApp.Start<Startup>(url))
            {
#if DEBUG
                Process.Start(url);
#endif
                Console.WriteLine("Angular Client UI en {0}", url);
                Console.ReadLine();
            }
        }
    }
}
