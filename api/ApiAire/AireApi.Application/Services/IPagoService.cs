using AireApi.Application.DTOs;
using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.Services
{
    public interface IPagoService
    {
        Task<IEnumerable<Pago>> GetByFacturaAsync(int idFactura);
        Task<int> CreateAsync(CreatePagoDto dto);
    }
}
