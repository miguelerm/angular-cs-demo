using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace AngularClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var rootPath = @".";
#if DEBUG
            rootPath = @"..\..";
            app.UseErrorPage();

            app.UseFileServer(new FileServerOptions()
            {
                RequestPath = new PathString("/bower_components"),
                FileSystem = new PhysicalFileSystem(rootPath + @"\bower_components"),
            });
#endif

            app.UseFileServer(new FileServerOptions()
            {
                RequestPath = PathString.Empty,
                FileSystem = new PhysicalFileSystem(rootPath + @"\wwwroot"),
            });
        }
    }
}
