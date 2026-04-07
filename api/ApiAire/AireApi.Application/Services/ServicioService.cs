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
    public class ServicioService : IServicioService
    {
        private readonly IServicioRepository _repo;

        public ServicioService(IServicioRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ServicioView>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<ServicioView?> GetByIdAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<ServicioView>> GetByClienteAsync(int idCliente)
            => await _repo.GetByClienteAsync(idCliente);

        public async Task<int> CreateAsync(CreateServicioDto dto)
        {
            var servicio = new Servicio
            {
                IdCliente = dto.IdCliente,
                IdUsuario = dto.IdUsuario,
                IdVehiculo = dto.IdVehiculo,
                Placa = dto.Placa,
                Diagnostico = dto.Diagnostico
            };
            return await _repo.CreateAsync(servicio);
        }

        public async Task<bool> UpdateEstadoAsync(int idServicio, int idEstadoServicio)
            => await _repo.UpdateEstadoAsync(idServicio, idEstadoServicio);

        public async Task<bool> DeleteAsync(int id)
        => await _repo.DeleteAsync(id);
    }
}
