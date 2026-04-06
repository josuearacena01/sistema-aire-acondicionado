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
    public class PagoRepository : IPagoRepository
    {
        private readonly DbConnectionFactory _db;

        public PagoRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Pago>> GetByFacturaAsync(int idFactura)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<Pago>(
                "SELECT * FROM aire.Pagos WHERE IdFactura = @IdFactura",
                new { IdFactura = idFactura });
        }

        public async Task<int> CreateAsync(Pago pago)
        {
            using var conn = _db.CreateConnection();
            var sql = @"INSERT INTO aire.Pagos (IdFactura, IdUsuario, MetodoPago, Monto)
            VALUES (@IdFactura, @IdUsuario, @MetodoPago, @Monto);
            SELECT CAST(SCOPE_IDENTITY() AS INT)";
            return await conn.QuerySingleAsync<int>(sql, pago);
        }
    }
}
