using AireApi.Domain.Entities;
using AireApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.Services
{
    public class CatalogoService : ICatalogoService
    {
        private readonly ICatalogoRepository _repo;

        public CatalogoService(ICatalogoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Provincia>> GetProvinciasAsync()
            => await _repo.GetProvinciasAsync();

        public async Task<IEnumerable<Marca>> GetMarcasAsync()
            => await _repo.GetMarcasAsync();

        public async Task<IEnumerable<Modelo>> GetModelosByMarcaAsync(int idMarca)
            => await _repo.GetModelosByMarcaAsync(idMarca);

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
            => await _repo.GetCategoriasAsync();

        public async Task<IEnumerable<TipoServicio>> GetTiposServicioAsync()
            => await _repo.GetTiposServicioAsync();

        public async Task<IEnumerable<EstadoServicio>> GetEstadosServicioAsync()
            => await _repo.GetEstadosServicioAsync();

        public async Task<IEnumerable<EstadoFactura>> GetEstadosFacturaAsync()
            => await _repo.GetEstadosFacturaAsync();
    }
}
