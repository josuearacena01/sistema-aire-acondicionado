using AireApi.Application.DTOs;
using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.Services
{
    public interface IDetalleServicioService
    {
        Task<IEnumerable<DetalleServicio>> GetByServicioAsync(int idServicio);
        Task<int> CreateAsync(CreateDetalleServicioDto dto);
        Task<bool> UpdateAsync(int id, CreateDetalleServicioDto dto);
        Task<bool> DeleteAsync(int id, int idServicio);
    }
}
