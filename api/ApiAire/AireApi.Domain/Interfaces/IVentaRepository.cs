using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Interfaces
{
    public interface IVentaRepository
    {
        Task<IEnumerable<dynamic>> GetAllAsync();
        Task<dynamic?> GetByIdAsync(int id);
        Task<dynamic?> RegistrarVentaAsync(int idCliente, int idUsuario, string productosJson, string metodoPago);
    }
}
