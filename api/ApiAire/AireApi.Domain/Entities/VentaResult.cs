using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Entities
{
    public class VentaResult
    {
        public int IdVenta { get; set; }
        public int IdFactura { get; set; }
        public decimal Total { get; set; }
    }
}