using CSharpRestBackend.Modelos;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CSharpRestBackend.Controllers
{
    public class OpcionesDeMenuController : ApiController
    {
        public IHttpActionResult Get()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Log.Debug("Autenticado como el usuario {userId}", userId);

            // TODO: consultar a la base de datos las opciones del menú para el usuario.
            return Ok(new[]{
                new OpcionDeMenu {
                    Titulo = "Bodegas",
                    Url = "#/bodegas"
                },
                new OpcionDeMenu {
                    Titulo = "Reportes",
                    Url = default(string),
                    Opciones = new [] {
                        new OpcionDeMenu{ Titulo = "Top 10", Url = "#/bodegas/reportes/top-10" },
                        new OpcionDeMenu{ Titulo = "Catalogo", Url = "#/bodegas/reportes/catalogo" }
                    }
                }
            });
        }
    }
}
