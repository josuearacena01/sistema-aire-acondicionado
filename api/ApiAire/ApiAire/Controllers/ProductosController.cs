using AireApi.Application.DTOs;
using AireApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAire.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _service;

    public ProductosController(IProductoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var producto = await _service.GetByIdAsync(id);
        return producto == null ? NotFound() : Ok(producto);
    }

    [HttpGet("categoria/{idCategoria}")]
    public async Task<IActionResult> GetByCategoria(int idCategoria)
        => Ok(await _service.GetByCategoriaAsync(idCategoria));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductoDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductoDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return result ? Ok() : NotFound();
    }
}