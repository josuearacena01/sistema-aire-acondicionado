using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Interfaces
{
    public interface IDetalleServicioRepository
    {
        Task<IEnumerable<DetalleServicio>> GetByServicioAsync(int idServicio);
        Task<int> CreateAsync(DetalleServicio detalle);
    }
}
