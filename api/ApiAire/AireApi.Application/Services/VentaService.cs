using AireApi.Application.DTOs;
using AireApi.Domain.Entities;
using AireApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AireApi.Application.Services
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _repo;

        public VentaService(IVentaRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Venta>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<Venta?> GetByIdAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task<VentaResult?> RegistrarVentaAsync(RegistrarVentaDto dto)
        {
            var productosJson = JsonSerializer.Serialize(dto.Productos);
            return await _repo.RegistrarVentaAsync(dto.IdCliente, dto.IdUsuario, productosJson, dto.MetodoPago);
        }
    }
}
