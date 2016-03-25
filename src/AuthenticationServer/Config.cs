using AuthenticationServer.Repositories;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Logging;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationServer
{
    public static class Config
    {
        public static void ConfigureAuthenticationServer(this IAppBuilder app, X509Certificate2 certificate)
        {
            //LogProvider.SetCurrentLogProvider(new CustomLogProvider());

            var users = UsersRepository.GetAll();
            var clients = ClientsRepository.GetAll();
            var scopes = ScopesRepository.GetAll();

            app.UseIdentityServer(new IdentityServerOptions
            {
                SiteName = "Authentication Server",
                RequireSsl = false,
                SigningCertificate = certificate,
                Factory = new IdentityServerServiceFactory()
                    .UseInMemoryUsers(users)
                    .UseInMemoryClients(clients)
                    .UseInMemoryScopes(scopes)
            });
        }
    }
}
