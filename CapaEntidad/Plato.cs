using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Plato
    {
        public int IdPlato { get; set; }
        public string Nombreplato { get; set;}
        public string Ingredientes { get; set;}
        public Categoria oDescripcion { get; set;}
        public decimal Precio { get; set; }
        public string PrecioTexto { get; set; }
        public string Rutaimagen { get; set; }
        public string Nombreimagen { get; set; }
        public bool Activo { get; set; }
        public string Base64 { get; set; }
        public string Extension { get; set; }

    }
}
