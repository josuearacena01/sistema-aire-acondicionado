using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Entities
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public int IdEstadoFactura { get; set; }
        public DateTime FechaFactura { get; set; }
        public decimal TotalFactura { get; set; }
    }

}
