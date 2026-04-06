using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.DTOs
{
    public class RegistrarVentaDto
    {
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public List<ProductoVentaDto> Productos { get; set; } = new();
    }
}
