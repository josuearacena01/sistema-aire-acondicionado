using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.DTOs
{
    public class CreateClienteDto
    {
        public int? IdProvincia { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "La cédula debe tener exactamente 11 dígitos")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "La cédula solo debe contener números")]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los nombres son obligatorios")]
        [StringLength(100, ErrorMessage = "Los nombres no pueden exceder 100 caracteres")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        [StringLength(100, ErrorMessage = "Los apellidos no pueden exceder 100 caracteres")]
        public string Apellidos { get; set; } = string.Empty;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe tener 10 dígitos")]
        public string? Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        public string? Correo { get; set; }

        public string? Sector { get; set; }
        public string? Ciudad { get; set; }
        public string? Calle { get; set; }

        [Required(ErrorMessage = "El username es obligatorio")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "El username debe tener entre 4 y 50 caracteres")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Password { get; set; } = string.Empty;
    }
}
