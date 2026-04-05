using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Interfaces
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<dynamic>> GetAllAsync();
        Task<dynamic?> GetByIdAsync(int id);
        Task<IEnumerable<dynamic>> GetByClienteAsync(int idCliente);
        Task<bool> AnularAsync(int idFactura);
    }
}
