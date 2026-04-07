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
    public class DetalleServicioService : IDetalleServicioService
    {
        private readonly IDetalleServicioRepository _repo;
        private readonly IServicioRepository _servicioRepo;

        public DetalleServicioService(IDetalleServicioRepository repo, IServicioRepository servicioRepo)
        {
            _repo = repo;
            _servicioRepo = servicioRepo;
        }

        public async Task<IEnumerable<DetalleServicio>> GetByServicioAsync(int idServicio)
            => await _repo.GetByServicioAsync(idServicio);

        public async Task<int> CreateAsync(CreateDetalleServicioDto dto)
        {
            var detalle = new DetalleServicio
            {
                IdServicio = dto.IdServicio,
                IdTipoServicio = dto.IdTipoServicio,
                PrecioServicio = dto.PrecioServicio
            };
            var id = await _repo.CreateAsync(detalle);

            // Recalcular total del servicio
            var detalles = await _repo.GetByServicioAsync(dto.IdServicio);
            var total = detalles.Sum(d => d.PrecioServicio);
            await _servicioRepo.UpdateTotalAsync(dto.IdServicio, total);

            return id;
        }

        public async Task<bool> UpdateAsync(int id, CreateDetalleServicioDto dto)
        {
            var detalle = new DetalleServicio
            {
                IdDetalleServicio = id,
                IdServicio = dto.IdServicio,
                IdTipoServicio = dto.IdTipoServicio,
                PrecioServicio = dto.PrecioServicio
            };
            var result = await _repo.UpdateAsync(detalle);

            if (result)
            {
                var detalles = await _repo.GetByServicioAsync(dto.IdServicio);
                var total = detalles.Sum(d => d.PrecioServicio);
                await _servicioRepo.UpdateTotalAsync(dto.IdServicio, total);
            }

            return result;
        }

        public async Task<bool> DeleteAsync(int id, int idServicio)
        {
            var result = await _repo.DeleteAsync(id);

            if (result)
            {
                var detalles = await _repo.GetByServicioAsync(idServicio);
                var total = detalles.Sum(d => d.PrecioServicio);
                await _servicioRepo.UpdateTotalAsync(idServicio, total);
            }

            return result;
        }
    }
}