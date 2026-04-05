using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Entities
{
    public class Log
    {
        public int IdLog { get; set; }
        public int? IdUsuario { get; set; }
        public string TipoLog { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaLog { get; set; }
    }

}
