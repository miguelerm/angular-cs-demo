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
            var httpConfiguration = new HttpConfiguration();

            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Filters.Add(new AuthorizeAttribute());

            var serializerSettings = httpConfiguration.Formatters.JsonFormatter.SerializerSettings;
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
            serializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;

            app.UseWebApi(httpConfiguration);
        }

        private static void ConfigurarAutenticacion(IAppBuilder app, X509Certificate2 certificado, string issuerName)
        {
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:15001",
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
