using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.DTOs;
using NominaSystem.Application.Interfaces;

namespace NominaSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReporteController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpPost("nomina-por-periodo")]
        public async Task<IActionResult> GenerarReporteNominaPorPeriodo([FromBody] ReporteNominaPeriodoRequest request)
        {
            var pdfBytes = await _reporteService.GenerarReporteNominaPorPeriodoAsync(request);
            var nombreArchivo = $"reporte_nomina_{DateTime.Now:yyyyMMddHHmmss}.pdf";

            return File(pdfBytes, "application/pdf", nombreArchivo);
        }

        [HttpPost("empleados-estado")]
        public async Task<IActionResult> GenerarReporteEmpleadosEstado([FromBody] ReporteEmpleadosEstadoRequest request)
        {
            var pdfBytes = await _reporteService.GenerarReporteEmpleadosEstadoAsync(request);
            return File(pdfBytes, "application/pdf", $"reporte_empleados_{request.Estado}_{DateTime.Now:yyyyMMddHHmmss}.pdf");
        }

        [HttpPost("reporte/descuentos")]
        public async Task<IActionResult> GenerarReporteDescuentos([FromBody] ReporteDescuentosRequest request)
        {
            var pdf = await _reporteService.GenerarReporteDescuentosAsync(request);
            return File(pdf, "application/pdf", $"reporte_descuentos_{DateTime.Now:yyyyMMddHHmmss}.pdf");
        }
        [HttpGet("expediente/{id}")]
        public async Task<IActionResult> DescargarExpediente(int id)
        {
            var pdfBytes = await _reporteService.GenerarExpedienteEmpleadoPdfAsync(id);
            return File(pdfBytes, "application/pdf", $"Expediente_Empleado_{id}.pdf");
        }

    
        [HttpGet("nomina/{id}")]
        public async Task<IActionResult> DescargarNominaIndividual(int id)
        {
            var pdfBytes = await _reporteService.GenerarNominaEmpleadoPdfAsync(id);

            if (pdfBytes == null)
                return NotFound("No se encontró la nómina.");

            return File(pdfBytes, "application/pdf", $"Nomina_Empleado_{id}.pdf");
        }

    }
}
