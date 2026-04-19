using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.DTOs
{
    public class UpdateClienteDto
    {
        public int? IdProvincia { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Sector { get; set; }
        public string? Ciudad { get; set; }
        public string? Calle { get; set; }
    }
}
