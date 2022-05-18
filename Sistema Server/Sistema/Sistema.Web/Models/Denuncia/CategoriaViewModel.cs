using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Denuncia
{
    public class CategoriaViewModel
    {

        public int iddenuncia { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cargo { get; set; }
        public string entidad { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string lugar { get; set; }
        public string descripcion { get; set; }
    }
}
