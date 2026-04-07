using AireApi.Application.DTOs;
using AireApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAire.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiciosController : ControllerBase
{
    private readonly IServicioService _service;

    public ServiciosController(IServicioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var servicio = await _service.GetByIdAsync(id);
        return servicio == null ? NotFound() : Ok(servicio);
    }

    [HttpGet("cliente/{idCliente}")]
    public async Task<IActionResult> GetByCliente(int idCliente)
        => Ok(await _service.GetByClienteAsync(idCliente));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServicioDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}/estado/{idEstadoServicio}")]
    public async Task<IActionResult> UpdateEstado(int id, int idEstadoServicio)
    {
        var result = await _service.UpdateEstadoAsync(id, idEstadoServicio);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        return result ? NoContent() : BadRequest("Solo se pueden eliminar servicios en estado Pendiente");
    }
}