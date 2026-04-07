using AireApi.Application.DTOs;
using AireApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAire.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiculosController : ControllerBase
{
    private readonly IVehiculoService _service;

    public VehiculosController(IVehiculoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var vehiculo = await _service.GetByIdAsync(id);
        return vehiculo == null ? NotFound() : Ok(vehiculo);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVehiculoDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }
}