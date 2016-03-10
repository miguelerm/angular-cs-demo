using AuthenticationServer.Repositories;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Logging;
using Owin;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace AuthenticationServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            LogProvider.SetCurrentLogProvider(new CustomLogProvider());

            var users = UsersRepository.GetAll();
            var clients = ClientsRepository.GetAll();
            var scopes = ScopesRepository.GetAll();

            app.UseIdentityServer(new IdentityServerOptions
            {
                SiteName = "Authentication Server",
                RequireSsl = false,
                SigningCertificate = LoadCertificate(),
                Factory = new IdentityServerServiceFactory()
                    .UseInMemoryUsers(users)
                    .UseInMemoryClients(clients)
                    .UseInMemoryScopes(scopes)
            });
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Certificados\idsrv3test.pfx"), "idsrv3test");
        }
    }

    internal class CustomLogProvider : ILogProvider
    {
        public Logger GetLogger(string name)
        {
            return GetLogger;
        }

        private static bool GetLogger(LogLevel logLevel, Func<string> messageFunc, Exception exception, params object[] formatParameters)
        {
            string result = null;
            if (messageFunc != null)
                result = messageFunc.Invoke();

            if (exception != null)
                Console.WriteLine("ERR: {0}", exception);

            if (result != null)
                Console.WriteLine("MSG: {0}", result);

            return true;
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
