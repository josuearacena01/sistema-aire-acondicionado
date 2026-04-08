using AireApi.Application.DTOs;
using AireApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAire.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _service;

    public UsuariosController(IUsuarioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var usuario = await _service.GetByIdAsync(id);
        return usuario == null ? NotFound() : Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUsuarioDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUsuarioDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return result ? NoContent() : NotFound();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var usuario = await _service.LoginAsync(dto);
        return usuario == null ? Unauthorized("Credenciales inválidas") : Ok(usuario);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deactivate(int id)
    {
        var result = await _service.DeactivateAsync(id);
        return result ? NoContent() : NotFound();
    }
}