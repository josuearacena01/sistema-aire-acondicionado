using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.DTOs
{
    public class CreateUsuarioDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 150 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El username es obligatorio")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "El username debe tener entre 4 y 50 caracteres")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es obligatorio")]
        [RegularExpression("^(Admin|Empleado)$", ErrorMessage = "El rol debe ser: Admin o Empleado")]
        public string Rol { get; set; } = string.Empty;
    }

}
