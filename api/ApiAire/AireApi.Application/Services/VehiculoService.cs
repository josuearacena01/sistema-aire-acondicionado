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
    public class VehiculoService : IVehiculoService
    {
        private readonly IVehiculoRepository _repo;

        public VehiculoService(IVehiculoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Vehiculo>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<Vehiculo?> GetByIdAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task<int> CreateAsync(CreateVehiculoDto dto)
        {
            var vehiculo = new Vehiculo
            {
                IdMarca = dto.IdMarca,
                IdModelo = dto.IdModelo,
                Anio = dto.Anio
            };
            return await _repo.CreateAsync(vehiculo);
        }

        public async Task<bool> UpdateAsync(int id, CreateVehiculoDto dto)
        {
            var vehiculo = await _repo.GetByIdAsync(id);
            if (vehiculo == null) return false;

            vehiculo.IdMarca = dto.IdMarca;
            vehiculo.IdModelo = dto.IdModelo;
            vehiculo.Anio = dto.Anio;

            return await _repo.UpdateAsync(vehiculo);
        }
    }
}
