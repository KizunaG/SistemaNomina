using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NominaSystem.Application.DTOs;
using NominaSystem.Domain.Entities;
using NominaSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using NominaSystem.Infrastructure.Data;

namespace NominaSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NominaController : ControllerBase
    {
        private readonly INominaService _service;
        private readonly ApplicationDbContext _context; // Inyección del DbContext

        // Inyección del servicio y DbContext
        public NominaController(INominaService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public async Task<List<NominaDto>> GetAllNominasAsync()
        {
            var nominas = await _context.Nominas
                .Include(n => n.Empleado)  // Asegúrate de incluir Empleado
                .Select(n => new NominaDto
                {
                    Id = n.Id,
                    NombreEmpleado = n.Empleado != null ? n.Empleado.Nombre : "N/A", // Asegúrate de que se asigna correctamente
                    PeriodoInicio = n.PeriodoInicio,
                    PeriodoFin = n.PeriodoFin,
                    SalarioBase = n.SalarioBase, // Verifica que estos valores estén siendo asignados correctamente
                    HorasExtras = n.HorasExtras,
                    Bonificaciones = n.Bonificaciones,
                    Descuentos = n.Descuentos,

                })
                .ToListAsync();
            return nominas;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var nomina = await _service.GetByIdAsync(id);
            return nomina == null ? NotFound() : Ok(nomina);
        }

        [HttpPost]
        public async Task<IActionResult> CrearNomina([FromBody] NominaDto nuevaNomina)
        {
            if (nuevaNomina == null)
            {
                return BadRequest("Datos inválidos");
            }

            // Mapeo de NominaDto a la entidad Nomina
            var nominaCreada = new Nomina
            {
                EmpleadoId = nuevaNomina.ID_Empleado, // Mapeamos la propiedad del DTO
                PeriodoInicio = nuevaNomina.PeriodoInicio,
                PeriodoFin = nuevaNomina.PeriodoFin,
                SalarioBase = nuevaNomina.SalarioBase,
                HorasExtras = nuevaNomina.HorasExtras,
                Bonificaciones = nuevaNomina.Bonificaciones,
                Descuentos = nuevaNomina.Descuentos,
                FechaPago = DateTime.Now // Puedes asignar la fecha actual o la que corresponda
            };

            _context.Nominas.Add(nominaCreada);
            await _context.SaveChangesAsync();

            // Retorna la URL del recurso recién creado
            return CreatedAtAction(nameof(GetById), new { id = nominaCreada.Id }, nominaCreada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Nomina nomina)
        {
            if (id != nomina.Id) return BadRequest();
            await _service.UpdateAsync(nomina);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("procesar")]
        public async Task<IActionResult> ProcesarNomina([FromBody] Nomina nomina)
        {
            var resultado = await _service.ProcesarNominaAsync(nomina);
            return Ok(resultado);
        }

        [HttpGet("validar-procesamiento/{empleadoId}")]
        public async Task<IActionResult> ValidarAntesDeProcesar(int empleadoId)
        {
            bool valido = await _service.ValidarAntesProcesarNominaAsync(empleadoId);
            return Ok(new { empleadoId, puedeProcesar = valido });
        }
    }
}
