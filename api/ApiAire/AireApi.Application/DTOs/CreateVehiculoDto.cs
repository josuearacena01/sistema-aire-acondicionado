using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.DTOs
{
    public class CreateVehiculoDto
    {
        public int IdMarca { get; set; }
        public int IdModelo { get; set; }
        public int Anio { get; set; }
    }
}
