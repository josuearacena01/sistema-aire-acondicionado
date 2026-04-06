using AireApi.Application.DTOs;
using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.Services
{
    public interface IServicioService
    {
        Task<IEnumerable<ServicioView>> GetAllAsync();
        Task<ServicioView?> GetByIdAsync(int id);
        Task<IEnumerable<ServicioView>> GetByClienteAsync(int idCliente);
        Task<int> CreateAsync(CreateServicioDto dto);
        Task<bool> UpdateEstadoAsync(int idServicio, int idEstadoServicio);
    }
}
