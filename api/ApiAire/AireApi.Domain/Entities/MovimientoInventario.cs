using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Entities
{
    public class MovimientoInventario
    {
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public DateTime FechaMovimiento { get; set; }
    }

}
