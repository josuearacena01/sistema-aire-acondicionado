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
    public class DetalleServicioRepository : IDetalleServicioRepository
    {
        private readonly DbConnectionFactory _db;

        public DetalleServicioRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<IEnumerable<DetalleServicio>> GetByServicioAsync(int idServicio)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<DetalleServicio>(
                @"SELECT IdDetalleServicio, IdServicio, IdTipoServicio, 
              Precio_Servicio AS PrecioServicio 
              FROM aire.DetalleServicios WHERE IdServicio = @IdServicio",
                new { IdServicio = idServicio });
        }

        public async Task<int> CreateAsync(DetalleServicio detalle)
        {
            using var conn = _db.CreateConnection();
            var sql = @"INSERT INTO aire.DetalleServicios (IdServicio, IdTipoServicio, Precio_Servicio)
            VALUES (@IdServicio, @IdTipoServicio, @PrecioServicio);
            SELECT CAST(SCOPE_IDENTITY() AS INT)";
            return await conn.QuerySingleAsync<int>(sql, detalle);
        }

        public async Task<bool> UpdateAsync(DetalleServicio detalle)
        {
            using var conn = _db.CreateConnection();
            var sql = @"UPDATE aire.DetalleServicios SET 
            IdTipoServicio = @IdTipoServicio, Precio_Servicio = @PrecioServicio
            WHERE IdDetalleServicio = @IdDetalleServicio";
            var rows = await conn.ExecuteAsync(sql, detalle);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = _db.CreateConnection();
            var sql = "DELETE FROM aire.DetalleServicios WHERE IdDetalleServicio = @Id";
            var rows = await conn.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}