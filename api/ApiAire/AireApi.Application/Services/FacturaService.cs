using AireApi.Domain.Entities;
using AireApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _repo;

        public FacturaService(IFacturaRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<FacturaView>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<FacturaView?> GetByIdAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<FacturaView>> GetByClienteAsync(int idCliente)
            => await _repo.GetByClienteAsync(idCliente);

        public async Task<bool> AnularAsync(int idFactura)
            => await _repo.AnularAsync(idFactura);
    }
}
