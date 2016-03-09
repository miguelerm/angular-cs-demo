using AuthenticationServer.Repositories;
using IdentityServer3.Core.Configuration;
using Owin;

namespace AuthenticationServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var users = UsersRepository.GetAll();
            var clients = ClientsRepository.GetAll();
            var scopes = ScopesRepository.GetAll();

            app.UseIdentityServer(new IdentityServerOptions
            {
                SiteName = "Authentication Server",
                RequireSsl = false,
                Factory = new IdentityServerServiceFactory()
                    .UseInMemoryUsers(users)
                    .UseInMemoryClients(clients)
                    .UseInMemoryScopes(scopes)
            });
        }
    }
}
