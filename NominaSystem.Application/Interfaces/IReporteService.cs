using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NominaSystem.Application.DTOs;
using NominaSystem.Application.Interfaces;

namespace NominaSystem.Application.Interfaces
{
    public interface IReporteService
    {
        Task<byte[]> GenerarReporteNominaPorPeriodoAsync(ReporteNominaPeriodoRequest request);
        Task<byte[]> GenerarReporteEmpleadosEstadoAsync(ReporteEmpleadosEstadoRequest request);
        Task<byte[]> GenerarReporteDescuentosAsync(ReporteDescuentosRequest request);
        Task<byte[]> GenerarExpedienteEmpleadoPdfAsync(int empleadoId);
        Task<byte[]> GenerarNominaEmpleadoPdfAsync(int nominaId);



    }
}



