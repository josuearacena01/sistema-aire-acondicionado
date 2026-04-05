using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Entities
{
    public class FacturaDetalle
    {
        public int IdFacturaDetalle { get; set; }
        public int IdFactura { get; set; }
        public int? IdVenta { get; set; }
        public int? IdServicio { get; set; }
        public string DescripcionDetalle { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }

}
