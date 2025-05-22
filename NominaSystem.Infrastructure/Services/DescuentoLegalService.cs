using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
using NominaSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace NominaSystem.Infrastructure.Services;

public class DescuentoLegalService : IDescuentoLegalService
{
    private readonly ApplicationDbContext _context;

    public DescuentoLegalService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DescuentoLegal>> GetAllAsync() =>
        await _context.DescuentosLegales.ToListAsync();

    public async Task<DescuentoLegal?> GetByIdAsync(int id) =>
        await _context.DescuentosLegales.FindAsync(id);

    public async Task AddAsync(DescuentoLegal descuento)
    {
        _context.DescuentosLegales.Add(descuento);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(DescuentoLegal descuento)
    {
        _context.DescuentosLegales.Update(descuento);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var descuento = await _context.DescuentosLegales.FindAsync(id);
        if (descuento != null)
        {
            _context.DescuentosLegales.Remove(descuento);
            await _context.SaveChangesAsync();
        }
    }
}
