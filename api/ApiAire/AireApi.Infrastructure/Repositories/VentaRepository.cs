using AireApi.Domain.Entities;
using AireApi.Domain.Interfaces;
using AireApi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AireApi.Infrastructure.Repositories
{
    public class VentaRepository : IVentaRepository
    {
        private readonly DbConnectionFactory _db;

        public VentaRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Venta>> GetAllAsync()
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<Venta>(
                @"SELECT IdVenta, IdCliente, IdUsuario, 
              Fecha_Venta AS FechaVenta, Total_Venta AS TotalVenta 
              FROM aire.Ventas");
        }

        public async Task<Venta?> GetByIdAsync(int id)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Venta>(
                @"SELECT IdVenta, IdCliente, IdUsuario, 
              Fecha_Venta AS FechaVenta, Total_Venta AS TotalVenta 
              FROM aire.Ventas WHERE IdVenta = @Id", new { Id = id });
        }

        public async Task<VentaResult?> RegistrarVentaAsync(int idCliente, int idUsuario, string productosJson, string metodoPago)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<VentaResult>(
                "aire.SP_RegistrarVenta",
                new
                {
                    IdCliente = idCliente,
                    IdUsuario = idUsuario,
                    Productos = productosJson,
                    MetodoPago = metodoPago
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}
