using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Entities
{
    public class ServicioView
    {
        public int IdServicio { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string? Diagnostico { get; set; }
        public decimal? TotalServicio { get; set; }
        public string EstadoServicio { get; set; } = string.Empty;
        public int IdCliente { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public int IdVehiculo { get; set; }
        public string Placa { get; set; } = string.Empty;
        public int Anio { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
    }
}