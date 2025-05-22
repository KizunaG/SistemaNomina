using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
using NominaSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace NominaSystem.Infrastructure.Services;

public class DetalleDescuentoNominaService : IDetalleDescuentoNominaService
{
    private readonly ApplicationDbContext _context;

    public DetalleDescuentoNominaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DetalleDescuentoNomina>> GetAllAsync() =>
        await _context.DetallesDescuentoNomina.ToListAsync();

    public async Task<DetalleDescuentoNomina?> GetByIdAsync(int id) =>
        await _context.DetallesDescuentoNomina.FindAsync(id);

    public async Task AddAsync(DetalleDescuentoNomina detalle)
    {
        _context.DetallesDescuentoNomina.Add(detalle);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(DetalleDescuentoNomina detalle)
    {
        _context.DetallesDescuentoNomina.Update(detalle);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var detalle = await _context.DetallesDescuentoNomina.FindAsync(id);
        if (detalle != null)
        {
            _context.DetallesDescuentoNomina.Remove(detalle);
            await _context.SaveChangesAsync();
        }
    }
}
