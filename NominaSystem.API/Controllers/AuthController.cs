using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NominaSystem.Application.DTOs;
using NominaSystem.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using NominaSystem.Domain.Entities;


namespace NominaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(ApplicationDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto request)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioNombre == request.UsuarioNombre);
        if (request.Contrasena != "1234")
            return Unauthorized("Credenciales incorrectas");


        var token = GenerarToken(usuario);

        return Ok(new { token });
    }

    private string GenerarToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario.UsuarioNombre),
            new Claim("id", usuario.Id.ToString()),
            new Claim("rol", usuario.ID_Rol.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
