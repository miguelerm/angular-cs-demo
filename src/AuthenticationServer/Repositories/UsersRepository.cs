using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthenticationServer.Repositories
{
    public class UsersRepository
    {
        public static List<InMemoryUser> GetAll()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "admin",
                    Password = "admin",
                    Subject = "101", // Este es el id único del usuario.

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Administrador"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Pérez"),
                        new Claim(Constants.ClaimTypes.Email, "admin.perez@lameratos.com")
                    }
                }
            };
        }
    }
}
