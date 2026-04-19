using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.DTOs
{
    public class CreateServicioDto
    {
        [Required(ErrorMessage = "El cliente es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un cliente válido")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un usuario válido")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El vehículo es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un vehículo válido")]
        public int IdVehiculo { get; set; }

        [Required(ErrorMessage = "La placa es obligatoria")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "La placa debe tener entre 6 y 10 caracteres")]
        public string Placa { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "El diagnóstico no puede exceder 500 caracteres")]
        public string? Diagnostico { get; set; }
    }
}
