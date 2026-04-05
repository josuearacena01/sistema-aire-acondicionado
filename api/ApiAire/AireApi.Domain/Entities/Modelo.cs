using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Entities
{
    public class Modelo
    {
        public int IdModelo { get; set; }
        public int IdMarca { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
