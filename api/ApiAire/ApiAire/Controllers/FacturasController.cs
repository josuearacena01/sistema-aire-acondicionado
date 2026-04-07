using AireApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAire.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FacturasController : ControllerBase
{
    private readonly IFacturaService _service;

    public FacturasController(IFacturaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var factura = await _service.GetByIdAsync(id);
        return factura == null ? NotFound() : Ok(factura);
    }

    [HttpGet("cliente/{idCliente}")]
    public async Task<IActionResult> GetByCliente(int idCliente)
        => Ok(await _service.GetByClienteAsync(idCliente));

    [HttpPut("{id}/anular")]
    public async Task<IActionResult> Anular(int id)
    {
        var result = await _service.AnularAsync(id);
        return result ? NoContent() : NotFound();
    }
}