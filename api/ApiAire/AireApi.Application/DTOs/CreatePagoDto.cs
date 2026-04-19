using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.DTOs
{
    public class CreatePagoDto
    {
        [Required(ErrorMessage = "La factura es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una factura válida")]
        public int IdFactura { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un usuario válido")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio")]
        [RegularExpression("^(Efectivo|Tarjeta|Transferencia)$", ErrorMessage = "El método de pago debe ser: Efectivo, Tarjeta o Transferencia")]
        public string MetodoPago { get; set; } = string.Empty;

        [Required(ErrorMessage = "El monto es obligatorio")]
        [Range(0.01, 999999.99, ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Monto { get; set; }
    }
}
