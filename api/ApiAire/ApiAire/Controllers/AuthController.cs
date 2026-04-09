using AireApi.Application.DTOs;
using AireApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiAire.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepo;
    private readonly IClienteRepository _clienteRepo;

    public AuthController(IUsuarioRepository usuarioRepo, IClienteRepository clienteRepo)
    {
        _usuarioRepo = usuarioRepo;
        _clienteRepo = clienteRepo;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUnificadoDto dto)
    {
        // Buscar primero en Usuarios (Admin/Empleado)
        var usuario = await _usuarioRepo.GetByUsernameAsync(dto.Username);
        if (usuario != null && usuario.Password == dto.Password && usuario.Estado == "Activo")
        {
            return Ok(new LoginResponseDto
            {
                Id = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Username = usuario.Username,
                Rol = usuario.Rol
            });
        }

        // Buscar en Clientes
        var cliente = await _clienteRepo.GetByUsernameAsync(dto.Username);
        if (cliente != null && cliente.Password == dto.Password)
        {
            return Ok(new LoginResponseDto
            {
                Id = cliente.IdCliente,
                Nombre = cliente.Nombres + " " + cliente.Apellidos,
                Username = cliente.Username,
                Rol = "Cliente"
            });
        }

        return Unauthorized("Credenciales inválidas");
    }
}