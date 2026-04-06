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
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _repo;

        public PagoService(IPagoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Pago>> GetByFacturaAsync(int idFactura)
            => await _repo.GetByFacturaAsync(idFactura);

        public async Task<int> CreateAsync(CreatePagoDto dto)
        {
            var pago = new Pago
            {
                IdFactura = dto.IdFactura,
                IdUsuario = dto.IdUsuario,
                MetodoPago = dto.MetodoPago,
                Monto = dto.Monto
            };
            return await _repo.CreateAsync(pago);
        }
    }
}
