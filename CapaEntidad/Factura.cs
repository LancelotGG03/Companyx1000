using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Factura
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public int Totalproducto { get; set; }
        public decimal ValorTotal { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

    }
}
