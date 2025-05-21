using Microsoft.EntityFrameworkCore;
using NominaSystem.Application.Interfaces;
using NominaSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Infrastructure.Services
{
    public class ExpedienteService : IExpedienteService
    {
        private readonly ApplicationDbContext _context;

        public ExpedienteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> ValidarExpedienteAsync(int empleadoId)
        {
            var obligatorios = await _context.ConfiguracionExpediente
                .Where(c => c.Obligatorio)
                .Select(c => c.TipoDocumento)
                .ToListAsync();

            var cargados = await _context.DocumentosEmpleado
                .Where(d => d.ID_Empleado == empleadoId)
                .Select(d => d.TipoDocumento)
                .ToListAsync();

            var faltantes = obligatorios.Except(cargados).ToList();

            if (!obligatorios.Any())
                return "Sin configuración";

            if (!faltantes.Any())
                return "Completo";

            if (cargados.Any())
                return "En proceso";

            return "Incompleto";
        }
    }
}
