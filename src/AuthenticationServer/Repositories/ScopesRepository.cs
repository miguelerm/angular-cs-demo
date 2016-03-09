using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace AuthenticationServer.Repositories
{
    public class ScopesRepository
    {
        public static List<Scope> GetAll()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.Email,

                new Scope
                {
                    Name = "api",

                    DisplayName = "Acceso a la REST API de C#",
                    Description = "Los clientes deben solicitar este scope para poder acceder a la API",
                
                    ScopeSecrets = new List<Secret>
                    {
                        new Secret("api-secret".Sha256())
                    },

                    Type = ScopeType.Resource
                }
            };
        }
    }
}
