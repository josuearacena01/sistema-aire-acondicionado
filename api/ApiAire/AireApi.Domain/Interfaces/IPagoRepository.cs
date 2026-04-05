using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Interfaces
{
    public interface IPagoRepository
    {
        Task<IEnumerable<Pago>> GetByFacturaAsync(int idFactura);
        Task<int> CreateAsync(Pago pago);
    }
}
