using AireApi.Application.DTOs;
using AireApi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Dapper;

namespace ApiAire.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly DbConnectionFactory _db;

    public ReportesController(DbConnectionFactory db)
    {
        _db = db;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
    {
        using var conn = _db.CreateConnection();

        // Ajustar fechaFin para incluir todo el día
        fechaFin = fechaFin.Date.AddDays(1).AddSeconds(-1);

        var totalVentas = await conn.QuerySingleAsync<int>(
            @"SELECT COUNT(*) FROM aire.Ventas 
              WHERE Fecha_Venta BETWEEN @FechaInicio AND @FechaFin",
            new { FechaInicio = fechaInicio, FechaFin = fechaFin });

        var totalIngresos = await conn.QuerySingleAsync<decimal>(
            @"SELECT ISNULL(SUM(Total_Venta), 0) FROM aire.Ventas 
              WHERE Fecha_Venta BETWEEN @FechaInicio AND @FechaFin",
            new { FechaInicio = fechaInicio, FechaFin = fechaFin });

        var totalServicios = await conn.QuerySingleAsync<int>(
            @"SELECT COUNT(*) FROM aire.Servicios 
              WHERE FechaIngreso BETWEEN @FechaInicio AND @FechaFin",
            new { FechaInicio = fechaInicio, FechaFin = fechaFin });

        var productosVendidos = await conn.QuerySingleAsync<int>(
            @"SELECT ISNULL(SUM(dv.Cantidad), 0) FROM aire.DetalleVentas dv
              INNER JOIN aire.Ventas v ON dv.IdVenta = v.IdVenta
              WHERE v.Fecha_Venta BETWEEN @FechaInicio AND @FechaFin",
            new { FechaInicio = fechaInicio, FechaFin = fechaFin });

        var productosMasVendidos = await conn.QueryAsync<ProductoMasVendidoDto>(
            @"SELECT TOP 10
                p.Nombre AS Producto,
                c.Nombre AS Categoria,
                SUM(dv.Cantidad) AS CantidadVendida,
                SUM(dv.Subtotal) AS TotalGenerado
              FROM aire.DetalleVentas dv
              INNER JOIN aire.Ventas v ON dv.IdVenta = v.IdVenta
              INNER JOIN aire.Productos p ON dv.IdProducto = p.IdProducto
              INNER JOIN aire.Categorias c ON p.IdCategoria = c.IdCategoria
              WHERE v.Fecha_Venta BETWEEN @FechaInicio AND @FechaFin
              GROUP BY p.Nombre, c.Nombre
              ORDER BY CantidadVendida DESC",
            new { FechaInicio = fechaInicio, FechaFin = fechaFin });

        var serviciosMasSolicitados = await conn.QueryAsync<ServicioMasSolicitadoDto>(
            @"SELECT TOP 10
                ts.Nombre AS Servicio,
                COUNT(*) AS VecesSolicitado,
                SUM(ds.Precio_Servicio) AS TotalGenerado
              FROM aire.DetalleServicios ds
              INNER JOIN aire.Servicios s ON ds.IdServicio = s.IdServicio
              INNER JOIN aire.TiposServicio ts ON ds.IdTipoServicio = ts.IdTipoServicio
              WHERE s.FechaIngreso BETWEEN @FechaInicio AND @FechaFin
              GROUP BY ts.Nombre
              ORDER BY VecesSolicitado DESC",
            new { FechaInicio = fechaInicio, FechaFin = fechaFin });

        var reporte = new ReporteDashboardDto
        {
            TotalVentas = totalVentas,
            TotalIngresos = totalIngresos,
            TotalServicios = totalServicios,
            ProductosVendidos = productosVendidos,
            ProductosMasVendidos = productosMasVendidos.ToList(),
            ServiciosMasSolicitados = serviciosMasSolicitados.ToList()
        };

        return Ok(reporte);
    }
}