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
    public class LogRepository : ILogRepository
    {
        private readonly DbConnectionFactory _db;

        public LogRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Log>> GetAllAsync()
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<Log>("SELECT * FROM aire.Logs ORDER BY FechaLog DESC");
        }

        public async Task<int> CreateAsync(Log log)
        {
            using var conn = _db.CreateConnection();
            var sql = @"INSERT INTO aire.Logs (IdUsuario, TipoLog, Descripcion)
            VALUES (@IdUsuario, @TipoLog, @Descripcion);
            SELECT CAST(SCOPE_IDENTITY() AS INT)";
            return await conn.QuerySingleAsync<int>(sql, log);
        }
    }
}
