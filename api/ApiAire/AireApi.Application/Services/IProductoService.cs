using AireApi.Application.DTOs;
using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.Services
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoView>> GetAllAsync();
        Task<ProductoView?> GetByIdAsync(int id);
        Task<IEnumerable<ProductoView>> GetByCategoriaAsync(int idCategoria);
        Task<int> CreateAsync(CreateProductoDto dto);
        Task<bool> UpdateAsync(int id, UpdateProductoDto dto);
        Task<bool> DeactivateAsync(int id);
    }
}
