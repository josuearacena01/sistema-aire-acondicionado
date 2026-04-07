using AireApi.Application.DTOs;
using AireApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAire.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _service;

    public ClientesController(IClienteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var cliente = await _service.GetByIdAsync(id);
        return cliente == null ? NotFound() : Ok(cliente);
    }

    [HttpGet("cedula/{cedula}")]
    public async Task<IActionResult> GetByCedula(string cedula)
    {
        var cliente = await _service.GetByCedulaAsync(cedula);
        return cliente == null ? NotFound() : Ok(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClienteDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateClienteDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return result ? Ok() : NotFound();
    }
}