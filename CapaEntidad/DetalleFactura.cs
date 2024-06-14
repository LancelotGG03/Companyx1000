using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleFactura
    {
        //        IdDetalleVenta int primary key identity,
        //IdVenta int references Factura(IdVenta),
        //IdProducto int references Plato(IdPlato),
        //Cantidad int,
        //Total decimal (10,0)
        public int IdDetalleVenta { get; set; }
        public Factura oVenta { get; set; }
        public Plato oPlato { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
    }
}
