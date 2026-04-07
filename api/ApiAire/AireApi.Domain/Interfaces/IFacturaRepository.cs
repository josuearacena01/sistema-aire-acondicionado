using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Interfaces
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<FacturaView>> GetAllAsync();
        Task<FacturaView?> GetByIdAsync(int id);
        Task<IEnumerable<FacturaView>> GetByClienteAsync(int idCliente);
        Task<bool> AnularAsync(int idFactura);
    }
}
