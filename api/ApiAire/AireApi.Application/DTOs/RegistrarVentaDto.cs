using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.DTOs
{
    public class RegistrarVentaDto
    {
        [Required(ErrorMessage = "El cliente es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un cliente válido")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un usuario válido")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio")]
        [RegularExpression("^(Efectivo|Tarjeta|Transferencia)$", ErrorMessage = "El método de pago debe ser: Efectivo, Tarjeta o Transferencia")]
        public string MetodoPago { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe enviar al menos un producto")]
        [MinLength(1, ErrorMessage = "Debe enviar al menos un producto")]
        public List<ProductoVentaDto> Productos { get; set; } = new();
    }
}
