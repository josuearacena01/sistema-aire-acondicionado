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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbConnectionFactory _db;

        public UsuarioRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<Usuario>("SELECT * FROM aire.Usuarios");
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Usuario>(
                "SELECT * FROM aire.Usuarios WHERE IdUsuario = @Id", new { Id = id });
        }

        public async Task<Usuario?> GetByUsernameAsync(string username)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Usuario>(
                "SELECT * FROM aire.Usuarios WHERE Username = @Username", new { Username = username });
        }

        public async Task<int> CreateAsync(Usuario usuario)
        {
            using var conn = _db.CreateConnection();
            var sql = @"INSERT INTO aire.Usuarios (Nombre, Username, Password, Rol)
            VALUES (@Nombre, @Username, @Password, @Rol);
            SELECT CAST(SCOPE_IDENTITY() AS INT)";
            return await conn.QuerySingleAsync<int>(sql, usuario);
        }

        public async Task<bool> UpdateAsync(Usuario usuario)
        {
            using var conn = _db.CreateConnection();
            var sql = @"UPDATE aire.Usuarios SET 
            Nombre = @Nombre, Password = @Password, Rol = @Rol, Estado = @Estado
            WHERE IdUsuario = @IdUsuario";
            var rows = await conn.ExecuteAsync(sql, usuario);
            return rows > 0;
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            using var conn = _db.CreateConnection();
            var sql = "UPDATE aire.Usuarios SET Estado = 'Inactivo' WHERE IdUsuario = @Id";
            var rows = await conn.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
