using AireApi.Application.DTOs;
using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente?> GetByIdAsync(int id);
        Task<Cliente?> GetByCedulaAsync(string cedula);
        Task<int> CreateAsync(CreateClienteDto dto);
        Task<bool> UpdateAsync(int id, UpdateClienteDto dto);
    }
}
