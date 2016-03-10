using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin.Cors;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace CSharpRestBackend
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Allow all origins
            app.UseCors(CorsOptions.AllowAll);

            ConfigurarAutenticacion(app);

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

        private static void ConfigurarAutenticacion(IAppBuilder app) {

            do
            {
                try
                {
                    Trace.TraceInformation("Intentando configurar el mecanismos de autenticación por tokens...");

                    // Wire token validation
                    app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
                    {
                        Authority = "http://localhost:15001",

                        // For access to the introspection endpoint
                        ClientId = "api",
                        ClientSecret = "api-secret",

                        RequiredScopes = new[] { "api" }
                    });

                    Trace.TraceInformation("Mecanismo de autenticación configurado.");
                    return;
                }
                catch (InvalidOperationException ex)
                {
                    var innerException = ex.InnerException as IOException;
                    if (innerException == null)
                    {
                        throw;
                    }

                    var innerExceptions = innerException.InnerException as AggregateException;

                    if (innerExceptions == null)
                    {
                        throw;
                    }

                    if (!innerExceptions.InnerExceptions.Any(x => x is HttpRequestException))
                    {
                        throw;
                    }
                }

                Trace.TraceError("No se pudo conectar con el AuthorityServer, intentando de nuevo en un segundo...");
                Thread.Sleep(TimeSpan.FromSeconds(1));

            } while (true);
           
        }
    }
}
