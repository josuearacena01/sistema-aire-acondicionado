using AireApi.Application.DTOs;
using AireApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAire.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetalleServiciosController : ControllerBase
{
    private readonly IDetalleServicioService _service;

    public DetalleServiciosController(IDetalleServicioService service)
    {
        _service = service;
    }

    [HttpGet("servicio/{idServicio}")]
    public async Task<IActionResult> GetByServicio(int idServicio)
        => Ok(await _service.GetByServicioAsync(idServicio));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDetalleServicioDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return Ok(new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateDetalleServicioDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id}/servicio/{idServicio}")]
    public async Task<IActionResult> Delete(int id, int idServicio)
    {
        var result = await _service.DeleteAsync(id, idServicio);
        return result ? NoContent() : NotFound();
    }
}