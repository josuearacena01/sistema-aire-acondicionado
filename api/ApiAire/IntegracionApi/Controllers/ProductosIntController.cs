using Microsoft.AspNetCore.Mvc;

namespace IntegracionApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosIntController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public ProductosIntController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Core");
    }

    [HttpGet]
    public async Task<IActionResult> GetProductos()
    {
        var response = await _httpClient.GetAsync("/api/productos");
        var content = await response.Content.ReadAsStringAsync();
        return Content(content, "application/json");
    }

    [HttpPost]
    public async Task<IActionResult> CreateProducto([FromBody] object producto)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/productos", producto);
        var content = await response.Content.ReadAsStringAsync();
        return Content(content, "application/json");
    }
}