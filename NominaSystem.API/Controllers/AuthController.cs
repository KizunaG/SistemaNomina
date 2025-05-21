using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;

namespace NominaSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _authService.ValidarCredencialesAsync(request.Correo, request.Contrasena);
            if (usuario == null)
                return Unauthorized("Credenciales inválidas");

            var token = _authService.GenerarToken(usuario);
            return Ok(new { token });
        }

        public class LoginRequest
        {
            public string Correo { get; set; }
            public string Contrasena { get; set; }
        }
    }
}
