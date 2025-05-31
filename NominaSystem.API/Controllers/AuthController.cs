using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NominaSystem.Application.DTOs;
using NominaSystem.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using NominaSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


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
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.UsuarioNombre == request.UsuarioNombre);

        if (usuario == null)
            return Unauthorized("Usuario no encontrado");

        // 🔐 Comparar la contraseña ingresada con la contraseña hasheada en la base de datos
        bool esValida = BCrypt.Net.BCrypt.Verify(request.Contrasena, usuario.Contrasena);

        if (!esValida)
            return Unauthorized("Contraseña incorrecta");

        // ✅ Generar el token
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
    [HttpPost("registrar")]
    [AllowAnonymous]
    public async Task<IActionResult> Registrar([FromBody] RegisterRequest request)
    {
        // Verificar si ya existe un usuario con ese nombre
        var existe = await _context.Usuarios
            .AnyAsync(u => u.UsuarioNombre == request.UsuarioNombre);

        if (existe)
            return BadRequest("El nombre de usuario ya está en uso.");

        var nuevoUsuario = new Usuario
        {
            UsuarioNombre = request.UsuarioNombre,
            Contrasena = BCrypt.Net.BCrypt.HashPassword(request.Contrasena), // Hashea la contraseña
            ID_Rol = 2, // Rol por defecto: 2 = Usuario
            ID_Empleado = 0 // Asociar después si es necesario
        };

        _context.Usuarios.Add(nuevoUsuario);
        await _context.SaveChangesAsync();

        return Ok("Usuario registrado correctamente.");
    }
}
