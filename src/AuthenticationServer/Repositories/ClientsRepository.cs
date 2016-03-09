using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace AuthenticationServer.Repositories
{
    class ClientsRepository
    {
        public static IEnumerable<Client> GetAll()
        {
            return new[]
            {
                new Client
                {
                    Enabled = true,
                    ClientName = "AngularClient",
                    ClientId = "angular-js",
                    Flow = Flows.Implicit,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:15000/autenticar.html",
                        "http://localhost:15000/reautenticar.html"
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:15000/index.html"
                    },

                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:15000"
                    },

                    AllowAccessToAllScopes = true,
                    AccessTokenLifetime = 70
                }
            };
        }
    }
}
