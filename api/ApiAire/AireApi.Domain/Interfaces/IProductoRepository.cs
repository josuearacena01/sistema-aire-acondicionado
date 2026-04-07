using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<ProductoView>> GetAllAsync();
        Task<ProductoView?> GetByIdAsync(int id);
        Task<IEnumerable<ProductoView>> GetByCategoriaAsync(int idCategoria);
        Task<int> CreateAsync(Producto producto);
        Task<bool> UpdateAsync(Producto producto);
        Task<bool> DeactivateAsync(int id);
    }
}
