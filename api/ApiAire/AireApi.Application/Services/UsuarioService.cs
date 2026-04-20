using AireApi.Application.DTOs;
using AireApi.Application.Helpers;
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
                Password = PasswordHasher.Hash(dto.Password),
                Rol = dto.Rol
            };
            return await _repo.CreateAsync(usuario);
        }

        public async Task<bool> UpdateAsync(int id, UpdateUsuarioDto dto)
        {
            var usuario = await _repo.GetByIdAsync(id);
            if (usuario == null) return false;

            if (!string.IsNullOrEmpty(dto.Nombre)) usuario.Nombre = dto.Nombre;
            if (!string.IsNullOrEmpty(dto.Rol)) usuario.Rol = dto.Rol;
            if (!string.IsNullOrEmpty(dto.Estado)) usuario.Estado = dto.Estado;
            if (!string.IsNullOrEmpty(dto.Password)) usuario.Password = PasswordHasher.Hash(dto.Password);

            return await _repo.UpdateAsync(usuario);
        }

        public async Task<Usuario?> LoginAsync(LoginUnificadoDto dto)
        {
        var usuario = await _repo.GetByUsernameAsync(dto.Username);
        if (usuario == null) return null;
        if (!PasswordHasher.Verify(dto.Password, usuario.Password)) return null;
        if (usuario.Estado != "Activo") return null;
        return usuario;
    }

        public async Task<bool> DeactivateAsync(int id)
        => await _repo.DeactivateAsync(id);
    }
}
