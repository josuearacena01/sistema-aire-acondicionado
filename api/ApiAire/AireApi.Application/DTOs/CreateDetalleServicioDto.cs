using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.DTOs
{
    public class CreateDetalleServicioDto
    {
        [Required(ErrorMessage = "El servicio es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un servicio válido")]
        public int IdServicio { get; set; }

        [Required(ErrorMessage = "El tipo de servicio es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de servicio válido")]
        public int IdTipoServicio { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0, 999999.99, ErrorMessage = "El precio no puede ser negativo")]
        public decimal PrecioServicio { get; set; }
    }
}
