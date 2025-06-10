using NominaSystem.Application.DTOs;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
using NominaSystem.Infrastructure.Data;
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
            // ✅ Calcular IGSS y TotalPago antes de guardar
            nomina.IGSS = Math.Round(nomina.SalarioBase * 0.0483m, 2);
            nomina.TotalPago = nomina.SalarioBase
                               + nomina.HorasExtras
                               + nomina.Bonificaciones
                               - nomina.Descuentos
                               - nomina.IGSS;

            nomina.FechaPago = DateTime.UtcNow;

            _context.Nominas.Add(nomina);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Nomina nomina)
        {
            // ✅ También recalcular IGSS y TotalPago al actualizar
            nomina.IGSS = Math.Round(nomina.SalarioBase * 0.0483m, 2);
            nomina.TotalPago = nomina.SalarioBase
                               + nomina.HorasExtras
                               + nomina.Bonificaciones
                               - nomina.Descuentos
                               - nomina.IGSS;

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

        // ✅ Método para procesar nómina automáticamente (ya estaba bien)
        public async Task<Nomina> ProcesarNominaAsync(Nomina nomina)
        {
            nomina.IGSS = Math.Round(nomina.SalarioBase * 0.0483m, 2);
            nomina.TotalPago = nomina.SalarioBase
                               + nomina.HorasExtras
                               + nomina.Bonificaciones
                               - nomina.Descuentos
                               - nomina.IGSS;

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

        // ✅ Método corregido para devolver IGSS
        public async Task<List<NominaDto>> GetAllNominasAsync()
        {
            return await _context.Nominas
                .Include(n => n.Empleado)
                .Select(n => new NominaDto
                {
                    Id = n.Id,
                    NombreEmpleado = n.Empleado != null ? n.Empleado.Nombre : "N/A",
                    PeriodoInicio = n.PeriodoInicio,
                    PeriodoFin = n.PeriodoFin,
                    SalarioBase = n.SalarioBase,
                    HorasExtras = n.HorasExtras,
                    Bonificaciones = n.Bonificaciones,
                    Descuentos = n.Descuentos,
                    IGSS = n.IGSS // ✅ Este campo es necesario para que TotalPago se calcule bien en el DTO
                    // TotalPago no se asigna porque es propiedad calculada en el DTO
                })
                .ToListAsync();
        }
    }
}

