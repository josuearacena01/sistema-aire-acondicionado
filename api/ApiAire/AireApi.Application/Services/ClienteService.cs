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
                Calle = dto.Calle
            };
            return await _repo.CreateAsync(cliente);
        }

        public async Task<bool> UpdateAsync(int id, UpdateClienteDto dto)
        {
            var cliente = await _repo.GetByIdAsync(id);
            if (cliente == null) return false;

            cliente.IdProvincia = dto.IdProvincia;
            cliente.Nombres = dto.Nombres;
            cliente.Apellidos = dto.Apellidos;
            cliente.Telefono = dto.Telefono;
            cliente.Correo = dto.Correo;
            cliente.Sector = dto.Sector;
            cliente.Ciudad = dto.Ciudad;
            cliente.Calle = dto.Calle;

            return await _repo.UpdateAsync(cliente);
        }
    }
}
