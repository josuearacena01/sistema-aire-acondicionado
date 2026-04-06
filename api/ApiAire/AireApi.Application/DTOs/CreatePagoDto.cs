using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.DTOs
{
    public class CreatePagoDto
    {
        public int IdFactura { get; set; }
        public int IdUsuario { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public decimal Monto { get; set; }
    }
}
