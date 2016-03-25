using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularClient
{
    public static class Config
    {
        public static void ConfigureClient(this IAppBuilder app, string root)
        {

#if DEBUG
            app.UseErrorPage();

            var bowerComponentsPath = Path.Combine(root, "bower_components");
            if (Directory.Exists(bowerComponentsPath))
            {
                app.UseFileServer(new FileServerOptions()
                {
                    RequestPath = new PathString("/bower_components"),
                    FileSystem = new PhysicalFileSystem(bowerComponentsPath),
                });
            }
            else
            {
                Log.Warning("No existe el directorio {path}, posiblemente requiera instalar las dependencias de bower en el directorio {root}.", bowerComponentsPath, root);
            }
#endif

            var wwwrootPath = Path.Combine(root, "wwwroot");
            if (Directory.Exists(wwwrootPath))
            {
                app.UseFileServer(new FileServerOptions()
                {
                    RequestPath = PathString.Empty,
                    FileSystem = new PhysicalFileSystem(wwwrootPath),
                });
            }
            else
            {
                throw new DirectoryNotFoundException($"El directorio {wwwrootPath} no existe.");
            }
            
        }
    }
}
