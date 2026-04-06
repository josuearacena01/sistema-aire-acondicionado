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
    public class CatalogoRepository : ICatalogoRepository
    {
        private readonly DbConnectionFactory _db;

        public CatalogoRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Provincia>> GetProvinciasAsync()
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<Provincia>("SELECT * FROM aire.Provincias");
        }

        public async Task<IEnumerable<Marca>> GetMarcasAsync()
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<Marca>("SELECT * FROM aire.Marcas");
        }

        public async Task<IEnumerable<Modelo>> GetModelosByMarcaAsync(int idMarca)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<Modelo>(
                "SELECT * FROM aire.Modelos WHERE IdMarca = @IdMarca",
                new { IdMarca = idMarca });
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<Categoria>("SELECT * FROM aire.Categorias");
        }

        public async Task<IEnumerable<TipoServicio>> GetTiposServicioAsync()
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<TipoServicio>("SELECT * FROM aire.TiposServicio");
        }

        public async Task<IEnumerable<EstadoServicio>> GetEstadosServicioAsync()
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<EstadoServicio>("SELECT * FROM aire.EstadosServicio");
        }

        public async Task<IEnumerable<EstadoFactura>> GetEstadosFacturaAsync()
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<EstadoFactura>("SELECT * FROM aire.EstadosFactura");
        }
    }
}
