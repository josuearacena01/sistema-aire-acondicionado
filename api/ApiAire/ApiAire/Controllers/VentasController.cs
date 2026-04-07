using AireApi.Application.DTOs;
using AireApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAire.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly IVentaService _service;

    public VentasController(IVentaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var venta = await _service.GetByIdAsync(id);
        return venta == null ? NotFound() : Ok(venta);
    }

    [HttpPost]
    public async Task<IActionResult> Registrar([FromBody] RegistrarVentaDto dto)
    {
        var result = await _service.RegistrarVentaAsync(dto);
        return result == null ? BadRequest("Error al registrar la venta") : Ok(result);
    }
}