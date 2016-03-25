using Microsoft.Owin.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Owin;
using AngularClient;
using AuthenticationServer;
using CSharpRestBackend;

namespace Host
{
    class Program
    {
        private static X509Certificate2 certificado;

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .MinimumLevel.Debug()
                .CreateLogger();

            certificado = LoadCertificate();

            Log.Debug("Usando certificado: {@certificado}", new { certificado.FriendlyName, certificado.Issuer });

            var url = "http://localhost:15001";

            using (WebApp.Start(url, Configure))
            {
                Console.WriteLine("Iniciado en {0}", url);
                Console.ReadKey();
            }
            
        }

        private static void Configure(IAppBuilder app)
        {
            app.ConfigureClient("../../../AngularClient");
            app.Map("/identity", identity => identity.ConfigureAuthenticationServer(certificado));
            app.Map("/api", api => api.ConfigureRestApi(certificado, "http://localhost:15001/identity"));
        }

        static X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Certificados\idsrv3test.pfx"), "idsrv3test");
        }
    }
}
