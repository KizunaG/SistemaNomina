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
                    SalarioBase = (n.Empleado != null && n.Empleado.Cargo != null) ? n.Empleado.Cargo.SalarioBase : 0, // Asigna el SalarioBase desde el Cargo del Empleado
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
            var nomina = await _context.Nominas
                .Include(n => n.Empleado)  // Asegúrate de incluir el empleado
                .FirstOrDefaultAsync(n => n.Id == id);

            if (nomina == null)
            {
                return NotFound();
            }

            var nominaDto = new NominaDto
            {
                Id = nomina.Id,
                NombreEmpleado = nomina.Empleado?.Nombre ?? "Empleado no encontrado",  // Asigna el nombre del empleado
                PeriodoInicio = nomina.PeriodoInicio,
                PeriodoFin = nomina.PeriodoFin,
                SalarioBase = nomina.SalarioBase,
                HorasExtras = nomina.HorasExtras,
                Bonificaciones = nomina.Bonificaciones,
                Descuentos = nomina.Descuentos
            };

            return Ok(nominaDto);
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
        public async Task<IActionResult> Update(int id, [FromBody] NominaDto nominaDto)
        {
            var nomina = await _context.Nominas.FindAsync(id);
            if (nomina == null)
            {
                return NotFound();
            }

            // Actualiza los valores de la nómina
            nomina.PeriodoInicio = nominaDto.PeriodoInicio;
            nomina.PeriodoFin = nominaDto.PeriodoFin;
            nomina.SalarioBase = nominaDto.SalarioBase;
            nomina.HorasExtras = nominaDto.HorasExtras;
            nomina.Bonificaciones = nominaDto.Bonificaciones;
            nomina.Descuentos = nominaDto.Descuentos;

            // Guarda los cambios en la base de datos
            _context.Nominas.Update(nomina);
            await _context.SaveChangesAsync();

            return NoContent();  // Devuelve NoContent si la actualización fue exitosa
        }
        [HttpGet("empleado/{empleadoId}/salario")]
        public async Task<IActionResult> GetSalarioBaseByEmpleado(int empleadoId)
        {
            var empleado = await _context.Empleados
                .Include(e => e.Cargo)  // Asegúrate de incluir el cargo del empleado
                .FirstOrDefaultAsync(e => e.Id == empleadoId);

            if (empleado == null || empleado.Cargo == null)
            {
                return NotFound("Empleado o cargo no encontrado");
            }

            return Ok(empleado.Cargo.SalarioBase);  // Devuelve el salario base del cargo del empleado
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
