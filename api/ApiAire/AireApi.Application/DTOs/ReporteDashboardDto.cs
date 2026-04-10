using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.DTOs;

public class ReporteDashboardDto
{
    public int TotalVentas { get; set; }
    public decimal TotalIngresos { get; set; }
    public int TotalServicios { get; set; }
    public int ProductosVendidos { get; set; }
    public List<ProductoMasVendidoDto> ProductosMasVendidos { get; set; } = new();
    public List<ServicioMasSolicitadoDto> ServiciosMasSolicitados { get; set; } = new();
}

public class ProductoMasVendidoDto
{
    public string Producto { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public int CantidadVendida { get; set; }
    public decimal TotalGenerado { get; set; }
}

public class ServicioMasSolicitadoDto
{
    public string Servicio { get; set; } = string.Empty;
    public int VecesSolicitado { get; set; }
    public decimal TotalGenerado { get; set; }
}