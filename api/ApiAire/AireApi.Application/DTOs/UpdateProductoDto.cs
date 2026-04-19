using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.DTOs
{
    public class UpdateProductoDto
    {
        public int? IdCategoria { get; set; }
        public string? Nombre { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock { get; set; }
        public string? Imagen { get; set; }
    }
}
