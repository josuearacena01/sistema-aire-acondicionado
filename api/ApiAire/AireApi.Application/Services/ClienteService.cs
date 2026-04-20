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
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repo;

        public ClienteService(IClienteRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<Cliente?> GetByIdAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task<Cliente?> GetByCedulaAsync(string cedula)
            => await _repo.GetByCedulaAsync(cedula);

        public async Task<int> CreateAsync(CreateClienteDto dto)
        {
            var cliente = new Cliente
            {
                IdProvincia = dto.IdProvincia,
                Cedula = dto.Cedula,
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                Sector = dto.Sector,
                Ciudad = dto.Ciudad,
                Calle = dto.Calle,
                Username = dto.Username,
                Password = PasswordHasher.Hash(dto.Password)
            };
            return await _repo.CreateAsync(cliente);
        }

        public async Task<bool> UpdateAsync(int id, UpdateClienteDto dto)
        {
            var cliente = await _repo.GetByIdAsync(id);
            if (cliente == null) return false;

            if (dto.IdProvincia.HasValue && dto.IdProvincia.Value > 0) cliente.IdProvincia = dto.IdProvincia;
            if (!string.IsNullOrEmpty(dto.Nombres)) cliente.Nombres = dto.Nombres;
            if (!string.IsNullOrEmpty(dto.Apellidos)) cliente.Apellidos = dto.Apellidos;
            if (dto.Telefono != null) cliente.Telefono = dto.Telefono;
            if (dto.Correo != null) cliente.Correo = dto.Correo;
            if (dto.Sector != null) cliente.Sector = dto.Sector;
            if (dto.Ciudad != null) cliente.Ciudad = dto.Ciudad;
            if (dto.Calle != null) cliente.Calle = dto.Calle;

            return await _repo.UpdateAsync(cliente);
        }
    }
}
