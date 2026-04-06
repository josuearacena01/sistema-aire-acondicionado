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
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly DbConnectionFactory _db;

        public VehiculoRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Vehiculo>> GetAllAsync()
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<Vehiculo>("SELECT * FROM aire.Vehiculos");
        }

        public async Task<Vehiculo?> GetByIdAsync(int id)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Vehiculo>(
                "SELECT * FROM aire.Vehiculos WHERE IdVehiculo = @Id", new { Id = id });
        }

        public async Task<int> CreateAsync(Vehiculo vehiculo)
        {
            using var conn = _db.CreateConnection();
            var sql = @"INSERT INTO aire.Vehiculos (IdMarca, IdModelo, Anio)
            VALUES (@IdMarca, @IdModelo, @Anio);
            SELECT CAST(SCOPE_IDENTITY() AS INT)";
            return await conn.QuerySingleAsync<int>(sql, vehiculo);
        }

        public async Task<bool> UpdateAsync(Vehiculo vehiculo)
        {
            using var conn = _db.CreateConnection();
            var sql = @"UPDATE aire.Vehiculos SET 
            IdMarca = @IdMarca, IdModelo = @IdModelo, Anio = @Anio
            WHERE IdVehiculo = @IdVehiculo";
            var rows = await conn.ExecuteAsync(sql, vehiculo);
            return rows > 0;
        }
    }
}
