﻿using IdentityServer3.Core.Models;
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
                    ClientId = "angular-client",
                    Flow = Flows.Implicit,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:15001/#/validar-token?",
                        "http://localhost:15001/actualizar-token.html"
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:15001/#/sesion-cerrada"
                    },

                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:15001"
                    },
                    RequireConsent = false,
                    AllowAccessToAllScopes = true,
                    AccessTokenLifetime = 70
                }
            };
        }
    }
}
