using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.DTOs;
public class CreateVehiculoDto
{
    [Required(ErrorMessage = "La marca es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una marca válida")]
    public int IdMarca { get; set; }

    [Required(ErrorMessage = "El modelo es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un modelo válido")]
    public int IdModelo { get; set; }

    [Required(ErrorMessage = "El año es obligatorio")]
    [Range(1950, 2100, ErrorMessage = "El año debe estar entre 1950 y 2100")]
    public int Anio { get; set; }
}