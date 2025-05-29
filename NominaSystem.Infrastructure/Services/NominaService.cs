using NominaSystem.Application.DTOs;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
using NominaSystem.Infrastructure.Data;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace NominaSystem.Infrastructure.Services
{
    public class NominaService : INominaService
    {
        private readonly ApplicationDbContext _context;

        public NominaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Nomina>> GetAllAsync() =>
            await _context.Nominas.ToListAsync();

        public async Task<Nomina?> GetByIdAsync(int id) =>
            await _context.Nominas.FindAsync(id);

        public async Task AddAsync(Nomina nomina)
        {
            _context.Nominas.Add(nomina);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Nomina nomina)
        {
            _context.Nominas.Update(nomina);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var nomina = await _context.Nominas.FindAsync(id);
            if (nomina != null)
            {
                _context.Nominas.Remove(nomina);
                await _context.SaveChangesAsync();
            }
        }

        // Método para procesar nómina automáticamente
        public async Task<Nomina> ProcesarNominaAsync(Nomina nomina)
        {
            nomina.TotalPago = nomina.SalarioBase + nomina.HorasExtras + nomina.Bonificaciones - nomina.Descuentos;
            nomina.FechaPago = DateTime.UtcNow;

            _context.Nominas.Add(nomina);
            await _context.SaveChangesAsync();
            return nomina;
        }

        public async Task<bool> ValidarAntesProcesarNominaAsync(int empleadoId)
        {
            var configuraciones = await _context.ConfiguracionExpedientes
                .Where(c => c.Obligatorio)
                .Select(c => c.TipoDocumento)
                .ToListAsync();

            var entregados = await _context.DocumentosEmpleado
                .Where(d => d.ID_Empleado == empleadoId)
                .Select(d => d.TipoDocumento)
                .ToListAsync();

            return configuraciones
                .Where(doc => doc != null)
                .All(doc => entregados.Where(e => e != null).Contains(doc));
        }

        // Nuevo método para obtener la lista de nóminas con información para mostrar en lista
        public async Task<List<NominaDto>> GetAllNominasAsync()
        {
            return await _context.Nominas
                .Include(n => n.Empleado)
                .Select(n => new NominaDto
                {
                    Id = n.Id,
                    NombreEmpleado = n.Empleado != null ? n.Empleado.Nombre : "N/A",
                    Periodo = $"{n.PeriodoInicio:dd/MM/yyyy} - {n.PeriodoFin:dd/MM/yyyy}",
                    TotalPago = n.TotalPago
                })
                .ToListAsync();
        }

    }
}
