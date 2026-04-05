using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Entities
{
    public class Servicio
    {
        public int IdServicio { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public int IdVehiculo { get; set; }
        public int IdEstadoServicio { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string? Diagnostico { get; set; }
        public decimal? TotalServicio { get; set; }
        public string Placa { get; set; } = string.Empty;
    }

}
