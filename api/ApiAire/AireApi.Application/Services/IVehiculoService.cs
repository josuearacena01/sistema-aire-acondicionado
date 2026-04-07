using AireApi.Application.DTOs;
using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.Services
{
    public interface IVehiculoService
    {
        Task<IEnumerable<Vehiculo>> GetAllAsync();
        Task<Vehiculo?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateVehiculoDto dto);
        Task<bool> UpdateAsync(int id, CreateVehiculoDto dto);
    }
}
