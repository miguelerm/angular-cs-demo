using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin.Cors;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web.Http;

namespace CSharpRestBackend
{
    public static class Config
    {
        public static void ConfigureRestApi(this IAppBuilder app, X509Certificate2 certificado, string issuerName)
        {
            // Allow all origins
            app.UseCors(CorsOptions.AllowAll);

            ConfigurarAutenticacion(app, certificado, issuerName);

            // Wire Web API
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.Filters.Add(new AuthorizeAttribute());

            var serializer = config.Formatters.JsonFormatter.SerializerSettings;
            serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializer.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
            serializer.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;

            config.Routes.MapHttpRoute(
                name: "default", 
                routeTemplate: "{controller}/{id}", 
                defaults: new { Id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }

        private static void ConfigurarAutenticacion(IAppBuilder app, X509Certificate2 certificado, string issuerName)
        {
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:56673/identity",
                IssuerName = issuerName,
                SigningCertificate = certificado,
                // For access to the introspection endpoint
                ClientId = "api",
                ClientSecret = "api-secret",

                RequiredScopes = new[] { "api" }
            });

        }
    }
}
