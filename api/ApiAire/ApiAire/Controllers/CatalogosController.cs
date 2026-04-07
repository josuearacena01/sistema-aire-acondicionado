using AireApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAire.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogosController : ControllerBase
{
    private readonly ICatalogoService _service;

    public CatalogosController(ICatalogoService service)
    {
        _service = service;
    }

    [HttpGet("provincias")]
    public async Task<IActionResult> GetProvincias()
        => Ok(await _service.GetProvinciasAsync());

    [HttpGet("marcas")]
    public async Task<IActionResult> GetMarcas()
        => Ok(await _service.GetMarcasAsync());

    [HttpGet("marcas/{idMarca}/modelos")]
    public async Task<IActionResult> GetModelos(int idMarca)
        => Ok(await _service.GetModelosByMarcaAsync(idMarca));

    [HttpGet("categorias")]
    public async Task<IActionResult> GetCategorias()
        => Ok(await _service.GetCategoriasAsync());

    [HttpGet("tipos-servicio")]
    public async Task<IActionResult> GetTiposServicio()
        => Ok(await _service.GetTiposServicioAsync());

    [HttpGet("estados-servicio")]
    public async Task<IActionResult> GetEstadosServicio()
        => Ok(await _service.GetEstadosServicioAsync());

    [HttpGet("estados-factura")]
    public async Task<IActionResult> GetEstadosFactura()
        => Ok(await _service.GetEstadosFacturaAsync());
}