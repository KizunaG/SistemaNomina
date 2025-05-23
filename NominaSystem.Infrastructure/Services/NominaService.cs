using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
using NominaSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

        // 👇 Método para procesar nómina automáticamente
        public async Task<Nomina> ProcesarNominaAsync(Nomina nomina)
        {
            nomina.TotalPago = nomina.SalarioBase + nomina.HorasExtras + nomina.Bonificaciones - nomina.Descuentos;
            nomina.FechaPago = DateTime.UtcNow;

            _context.Nominas.Add(nomina);
            await _context.SaveChangesAsync();
            return nomina;
        }
    }
}
