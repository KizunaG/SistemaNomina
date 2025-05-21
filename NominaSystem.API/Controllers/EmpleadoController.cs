namespace NominaSystem.API.Controllers
{
    using global::NominaSystem.Application.Interfaces;
    using global::NominaSystem.Domain.Entities;
    using Microsoft.AspNetCore.Mvc;

    namespace NominaSystem.API.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class EmpleadoController : ControllerBase
        {
            private readonly IEmpleadoService _empleadoService;

            public EmpleadoController(IEmpleadoService empleadoService)
            {
                _empleadoService = empleadoService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var empleados = await _empleadoService.ObtenerTodosAsync();
                return Ok(empleados);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id)
            {
                var empleado = await _empleadoService.ObtenerPorIdAsync(id);
                if (empleado == null) return NotFound();
                return Ok(empleado);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] Empleado empleado)
            {
                var creado = await _empleadoService.CrearAsync(empleado);
                return CreatedAtAction(nameof(Get), new { id = creado.Id }, creado);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromBody] Empleado empleado)
            {
                if (id != empleado.Id) return BadRequest();
                var actualizado = await _empleadoService.ActualizarAsync(empleado);
                if (!actualizado) return NotFound();
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var eliminado = await _empleadoService.EliminarAsync(id);
                if (!eliminado) return NotFound();
                return NoContent();
            }
        }
    }

}
