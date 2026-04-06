using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.DTOs
{
    public class UpdateUsuarioDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Password { get; set; }
        public string Rol { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
