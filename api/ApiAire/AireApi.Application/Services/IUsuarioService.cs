using AireApi.Application.DTOs;
using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateUsuarioDto dto);
        Task<bool> UpdateAsync(int id, UpdateUsuarioDto dto);
        Task<Usuario?> LoginAsync(LoginDto dto);
    }
}
