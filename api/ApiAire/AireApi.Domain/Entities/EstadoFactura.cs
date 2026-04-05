using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Entities
{
    public class EstadoFactura
    {
        public int IdEstadoFactura { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
