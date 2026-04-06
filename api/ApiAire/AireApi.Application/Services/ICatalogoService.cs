using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.Services
{
    public interface ICatalogoService
    {
        Task<IEnumerable<Provincia>> GetProvinciasAsync();
        Task<IEnumerable<Marca>> GetMarcasAsync();
        Task<IEnumerable<Modelo>> GetModelosByMarcaAsync(int idMarca);
        Task<IEnumerable<Categoria>> GetCategoriasAsync();
        Task<IEnumerable<TipoServicio>> GetTiposServicioAsync();
        Task<IEnumerable<EstadoServicio>> GetEstadosServicioAsync();
        Task<IEnumerable<EstadoFactura>> GetEstadosFacturaAsync();
    }
}
