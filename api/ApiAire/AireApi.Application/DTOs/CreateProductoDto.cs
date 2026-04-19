using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.DTOs;
public class CreateProductoDto
{
    [Required(ErrorMessage = "La categoría es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría válida")]
    public int IdCategoria { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 150 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
    public int Stock { get; set; }

    public string? Imagen { get; set; }
}