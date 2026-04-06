using AireApi.Application.DTOs;
using AireApi.Domain.Entities;
using AireApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<Usuario?> GetByIdAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task<int> CreateAsync(CreateUsuarioDto dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Username = dto.Username,
                Password = dto.Password,
                Rol = dto.Rol
            };
            return await _repo.CreateAsync(usuario);
        }

        public async Task<bool> UpdateAsync(int id, UpdateUsuarioDto dto)
        {
            var usuario = await _repo.GetByIdAsync(id);
            if (usuario == null) return false;

            usuario.Nombre = dto.Nombre;
            usuario.Rol = dto.Rol;
            usuario.Estado = dto.Estado;

            if (!string.IsNullOrEmpty(dto.Password))
                usuario.Password = dto.Password;

            return await _repo.UpdateAsync(usuario);
        }

        public async Task<Usuario?> LoginAsync(LoginDto dto)
        {
            var usuario = await _repo.GetByUsernameAsync(dto.Username);
            if (usuario == null) return null;
            if (usuario.Password != dto.Password) return null;
            if (usuario.Estado != "Activo") return null;
            return usuario;
        }
    }
}
