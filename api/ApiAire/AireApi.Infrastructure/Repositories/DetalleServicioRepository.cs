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
                "SELECT * FROM aire.DetalleServicios WHERE IdServicio = @IdServicio",
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
    }
}
