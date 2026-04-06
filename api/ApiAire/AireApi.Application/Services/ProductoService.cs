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
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repo;

        public ProductoService(IProductoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProductoView>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<ProductoView?> GetByIdAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<ProductoView>> GetByCategoriaAsync(int idCategoria)
            => await _repo.GetByCategoriaAsync(idCategoria);

        public async Task<int> CreateAsync(CreateProductoDto dto)
        {
            var producto = new Producto
            {
                IdCategoria = dto.IdCategoria,
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Stock = dto.Stock,
                Imagen = dto.Imagen
            };
            return await _repo.CreateAsync(producto);
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductoDto dto)
        {
            var producto = new Producto
            {
                IdProducto = id,
                IdCategoria = dto.IdCategoria,
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Stock = dto.Stock,
                Imagen = dto.Imagen
            };
            return await _repo.UpdateAsync(producto);
        }
    }
}
