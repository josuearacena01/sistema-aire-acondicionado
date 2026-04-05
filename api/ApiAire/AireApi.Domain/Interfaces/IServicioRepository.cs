using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Interfaces
{
    public interface IServicioRepository
    {
        Task<IEnumerable<dynamic>> GetAllAsync();
        Task<dynamic?> GetByIdAsync(int id);
        Task<IEnumerable<dynamic>> GetByClienteAsync(int idCliente);
        Task<int> CreateAsync(Servicio servicio);
        Task<bool> UpdateEstadoAsync(int idServicio, int idEstadoServicio);
        Task<bool> UpdateTotalAsync(int idServicio, decimal total);
    }
}