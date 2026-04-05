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
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Producto?> GetByIdAsync(int id);
        Task<IEnumerable<Producto>> GetByCategoriaAsync(int idCategoria);
        Task<int> CreateAsync(Producto producto);
        Task<bool> UpdateAsync(Producto producto);
    }

}
