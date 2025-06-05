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
                .Include(n => n.Empleado)
                .Select(n => new NominaDto
                {
                    Id = n.Id,
                    NombreEmpleado = n.Empleado != null ? n.Empleado.Nombre : "N/A",
                    PeriodoInicio = n.PeriodoInicio,
                    PeriodoFin = n.PeriodoFin,
                    SalarioBase = n.SalarioBase,        // ✅ Usar el guardado
                    HorasExtras = n.HorasExtras,
                    Bonificaciones = n.Bonificaciones,
                    Descuentos = n.Descuentos,
                    IGSS = n.IGSS                       // ✅ Este campo es clave
                                                        // TotalPago se calcula automáticamente en el DTO
                })
                .ToListAsync();

            return nominas;
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var nomina = await _context.Nominas
                .Include(n => n.Empleado)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (nomina == null)
                return NotFound();

            var dto = new NominaDto
            {
                Id = nomina.Id,
                ID_Empleado = nomina.EmpleadoId,
                NombreEmpleado = nomina.Empleado?.Nombre ?? "Empleado no encontrado",
                PeriodoInicio = nomina.PeriodoInicio,
                PeriodoFin = nomina.PeriodoFin,
                SalarioBase = nomina.SalarioBase,
                HorasExtras = nomina.HorasExtras,
                Bonificaciones = nomina.Bonificaciones,
                Descuentos = nomina.Descuentos,
                IGSS = nomina.IGSS
                // TotalPago se calcula automáticamente en el DTO
            };

            return Ok(dto);
        }




        [HttpPost]
        public async Task<IActionResult> CrearNomina([FromBody] NominaDto nuevaNomina)
        {
            if (nuevaNomina == null)
            {
                return BadRequest("Datos inválidos");
            }

            // ✅ Mapear el DTO a la entidad y calcular IGSS y TotalPago en el servicio
            var nominaCreada = new Nomina
            {
                EmpleadoId = nuevaNomina.ID_Empleado,
                PeriodoInicio = nuevaNomina.PeriodoInicio,
                PeriodoFin = nuevaNomina.PeriodoFin,
                SalarioBase = nuevaNomina.SalarioBase,
                HorasExtras = nuevaNomina.HorasExtras,
                Bonificaciones = nuevaNomina.Bonificaciones,
                Descuentos = nuevaNomina.Descuentos
                // IGSS y TotalPago se calcularán dentro del servicio
            };

            await _service.AddAsync(nominaCreada); // ✅ Usar servicio para que calcule y guarde

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

        [HttpGet("por-empleado/{empleadoId}")]
        public async Task<IActionResult> ObtenerNominasPorEmpleado(int empleadoId)
        {
            var nominas = await _context.Nominas
                .Include(n => n.Empleado)
                .Where(n => n.EmpleadoId == empleadoId)
                .Select(n => new NominaDto
                {
                    Id = n.Id,
                    ID_Empleado = n.EmpleadoId,
                    NombreEmpleado = n.Empleado.Nombre,
                    PeriodoInicio = n.PeriodoInicio,
                    PeriodoFin = n.PeriodoFin,
                    SalarioBase = n.SalarioBase,
                    HorasExtras = n.HorasExtras,
                    Bonificaciones = n.Bonificaciones,
                    Descuentos = n.Descuentos,
                })
                .ToListAsync();

            return Ok(nominas);
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

        [HttpGet("reporte-general")]
        public async Task<IActionResult> GenerarReporteGeneral()
        {
            var nominas = await _service.GetAllNominasAsync();

            var generador = new DocumentoReporteGeneralNominas(nominas);
            var pdfBytes = generador.Generar();

            return File(pdfBytes, "application/pdf", "ReporteGeneralNominas.pdf");
        }

    }
}
