
using global::NominaSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.Interfaces;

namespace NominaSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpedienteController : ControllerBase
    {
        private readonly IExpedienteService _expedienteService;

        public ExpedienteController(IExpedienteService expedienteService)
        {
            _expedienteService = expedienteService;
        }

        [HttpGet("validar/{empleadoId}")]
        public async Task<IActionResult> Validar(int empleadoId)
        {
            var estado = await _expedienteService.ValidarExpedienteAsync(empleadoId);
            return Ok(new { estado });
        }
    }
}
