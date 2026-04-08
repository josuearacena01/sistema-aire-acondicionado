using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario?> GetByUsernameAsync(string username);
        Task<int> CreateAsync(Usuario usuario);
        Task<bool> UpdateAsync(Usuario usuario);
        Task<bool> DeactivateAsync(int id);
    }
}