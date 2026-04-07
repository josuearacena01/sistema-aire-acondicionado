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
    public class FacturaRepository : IFacturaRepository
    {
        private readonly DbConnectionFactory _db;

        public FacturaRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<IEnumerable<FacturaView>> GetAllAsync()
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<FacturaView>("SELECT * FROM aire.vw_Facturas");
        }

        public async Task<FacturaView?> GetByIdAsync(int id)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<FacturaView>(
                "SELECT * FROM aire.vw_Facturas WHERE IdFactura = @Id", new { Id = id });
        }

        public async Task<IEnumerable<FacturaView>> GetByClienteAsync(int idCliente)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<FacturaView>(
                "SELECT * FROM aire.vw_Facturas WHERE IdCliente = @IdCliente",
                new { IdCliente = idCliente });
        }

        public async Task<bool> AnularAsync(int idFactura)
        {
            using var conn = _db.CreateConnection();
            var sql = "UPDATE aire.Facturas SET IdEstadoFactura = 3 WHERE IdFactura = @IdFactura";
            var rows = await conn.ExecuteAsync(sql, new { IdFactura = idFactura });
            return rows > 0;
        }
    }
}
