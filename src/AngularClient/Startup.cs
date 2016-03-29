using AuthenticationServer;
using CSharpRestBackend;
using Owin;
using System.Security.Cryptography.X509Certificates;
using System.Web.Hosting;

namespace AngularClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var certificado = CargarCertificado();
            app.Map("/identity", identity => identity.ConfigureAuthenticationServer(certificado));
            app.Map("/api", api => api.ConfigureRestApi(certificado, "http://localhost:15001/identity"));
        }

        static X509Certificate2 CargarCertificado()
        {
            var fileName = HostingEnvironment.MapPath("~/App_Data/Certificados/idsrv3test.pfx");
            var password = "idsrv3test";
            return new X509Certificate2(fileName, password);
        }
    }
}