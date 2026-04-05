using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Entities
{
    public class DetalleServicio
    {
        public int IdDetalleServicio { get; set; }
        public int IdServicio { get; set; }
        public int IdTipoServicio { get; set; }
        public decimal PrecioServicio { get; set; }
    }

}
