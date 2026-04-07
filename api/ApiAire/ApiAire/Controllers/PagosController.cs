using AireApi.Application.DTOs;
using AireApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAire.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagosController : ControllerBase
{
    private readonly IPagoService _service;

    public PagosController(IPagoService service)
    {
        _service = service;
    }

    [HttpGet("factura/{idFactura}")]
    public async Task<IActionResult> GetByFactura(int idFactura)
        => Ok(await _service.GetByFacturaAsync(idFactura));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePagoDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return Ok(new { id });
    }
}