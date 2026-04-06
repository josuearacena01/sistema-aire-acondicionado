using AireApi.Domain.Entities;
using AireApi.Domain.Interfaces;
using AireApi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AireApi.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly DbConnectionFactory _db;

        public ProductoRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ProductoView>> GetAllAsync()
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<ProductoView>("SELECT * FROM aire.vw_Productos");
        }

        public async Task<ProductoView?> GetByIdAsync(int id)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<ProductoView>(
                "SELECT * FROM aire.vw_Productos WHERE IdProducto = @Id", new { Id = id });
        }

        public async Task<IEnumerable<ProductoView>> GetByCategoriaAsync(int idCategoria)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<ProductoView>(
                "SELECT * FROM aire.vw_Productos WHERE IdCategoria = @IdCategoria",
                new { IdCategoria = idCategoria });
        }

        public async Task<int> CreateAsync(Producto producto)
        {
            using var conn = _db.CreateConnection();
            var sql = @"INSERT INTO aire.Productos (IdCategoria, Nombre, Precio, Stock, Imagen)
            VALUES (@IdCategoria, @Nombre, @Precio, @Stock, @Imagen);
            SELECT CAST(SCOPE_IDENTITY() AS INT)";
            return await conn.QuerySingleAsync<int>(sql, producto);
        }

        public async Task<bool> UpdateAsync(Producto producto)
        {
            using var conn = _db.CreateConnection();
            var sql = @"UPDATE aire.Productos SET 
            IdCategoria = @IdCategoria, Nombre = @Nombre, Precio = @Precio, 
            Stock = @Stock, Imagen = @Imagen
            WHERE IdProducto = @IdProducto";
            var rows = await conn.ExecuteAsync(sql, producto);
            return rows > 0;
        }
    }
}
