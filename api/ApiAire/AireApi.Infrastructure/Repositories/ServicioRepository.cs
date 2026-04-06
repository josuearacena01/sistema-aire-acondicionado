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
    public class ServicioRepository : IServicioRepository
    {
        private readonly DbConnectionFactory _db;

        public ServicioRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ServicioView>> GetAllAsync()
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<ServicioView>("SELECT * FROM aire.vw_Servicios");
        }

        public async Task<ServicioView?> GetByIdAsync(int id)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<ServicioView>(
                "SELECT * FROM aire.vw_Servicios WHERE IdServicio = @Id", new { Id = id });
        }

        public async Task<IEnumerable<ServicioView>> GetByClienteAsync(int idCliente)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<ServicioView>(
                "SELECT * FROM aire.vw_Servicios WHERE IdCliente = @IdCliente",
                new { IdCliente = idCliente });
        }

        public async Task<int> CreateAsync(Servicio servicio)
        {
            using var conn = _db.CreateConnection();
            var sql = @"INSERT INTO aire.Servicios 
            (IdCliente, IdUsuario, IdVehiculo, IdEstadoServicio, Diagnostico, Placa)
            VALUES 
            (@IdCliente, @IdUsuario, @IdVehiculo, 1, @Diagnostico, @Placa);
            SELECT CAST(SCOPE_IDENTITY() AS INT)";
            return await conn.QuerySingleAsync<int>(sql, servicio);
        }

        public async Task<bool> UpdateEstadoAsync(int idServicio, int idEstadoServicio)
        {
            using var conn = _db.CreateConnection();
            var sql = "UPDATE aire.Servicios SET IdEstadoServicio = @IdEstadoServicio WHERE IdServicio = @IdServicio";
            var rows = await conn.ExecuteAsync(sql, new { IdServicio = idServicio, IdEstadoServicio = idEstadoServicio });
            return rows > 0;
        }

        public async Task<bool> UpdateTotalAsync(int idServicio, decimal total)
        {
            using var conn = _db.CreateConnection();
            var sql = "UPDATE aire.Servicios SET Total_Servicio = @Total WHERE IdServicio = @IdServicio";
            var rows = await conn.ExecuteAsync(sql, new { IdServicio = idServicio, Total = total });
            return rows > 0;
        }
    }
}
