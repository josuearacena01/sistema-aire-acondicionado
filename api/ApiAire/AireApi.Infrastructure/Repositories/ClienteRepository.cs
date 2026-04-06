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
    public class ClienteRepository : IClienteRepository
    {
        private readonly DbConnectionFactory _db;

        public ClienteRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<Cliente>("SELECT * FROM aire.Clientes");
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Cliente>(
                "SELECT * FROM aire.Clientes WHERE IdCliente = @Id", new { Id = id });
        }

        public async Task<Cliente?> GetByCedulaAsync(string cedula)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Cliente>(
                "SELECT * FROM aire.Clientes WHERE Cedula = @Cedula", new { Cedula = cedula });
        }

        public async Task<int> CreateAsync(Cliente cliente)
        {
            using var conn = _db.CreateConnection();
            var sql = @"INSERT INTO aire.Clientes 
            (IdProvincia, Cedula, Nombres, Apellidos, Telefono, Correo, Sector, Ciudad, Calle)
            VALUES 
            (@IdProvincia, @Cedula, @Nombres, @Apellidos, @Telefono, @Correo, @Sector, @Ciudad, @Calle);
            SELECT CAST(SCOPE_IDENTITY() AS INT)";
            return await conn.QuerySingleAsync<int>(sql, cliente);
        }

        public async Task<bool> UpdateAsync(Cliente cliente)
        {
            using var conn = _db.CreateConnection();
            var sql = @"UPDATE aire.Clientes SET 
            IdProvincia = @IdProvincia,
            Nombres = @Nombres,
            Apellidos = @Apellidos,
            Telefono = @Telefono,
            Correo = @Correo,
            Sector = @Sector,
            Ciudad = @Ciudad,
            Calle = @Calle
            WHERE IdCliente = @IdCliente";
            var rows = await conn.ExecuteAsync(sql, cliente);
            return rows > 0;
        }
    }
}
