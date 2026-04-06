using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.DTOs
{
    public class CreateServicioDto
    {
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public int IdVehiculo { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string? Diagnostico { get; set; }
    }
}
