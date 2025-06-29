using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Models
{
    public class CarritoConDetalleDTO
    {
        public int IdUsuario { get; set; }

        public double PrecioTotalCarrito { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public bool Estado { get; set; }

        public int IdLibro { get; set; }

        public double PrecioTotalDetalleCarrito { get; set; }

        public DateTime FechaFactura { get; set; }

        public string DetalleFactura { get; set; } = string.Empty;

        public DateTime FechaCreacionFactura { get; set; }
    }
}
