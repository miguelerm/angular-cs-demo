using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRestBackend.Modelos
{
    public class OpcionDeMenu
    {
        public string Titulo { get; set; }
        public string Url { get; set; }
        public ICollection<OpcionDeMenu> Opciones { get; set; }
    }
}
