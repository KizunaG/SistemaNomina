using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
using NominaSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace NominaSystem.Infrastructure.Services;

public class AjusteNominaService : IAjusteNominaService
{
    private readonly ApplicationDbContext _context;

    public AjusteNominaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AjusteNomina>> GetAllAsync() =>
        await _context.AjustesNomina.ToListAsync();

    public async Task<AjusteNomina?> GetByIdAsync(int id) =>
        await _context.AjustesNomina.FindAsync(id);

    public async Task AddAsync(AjusteNomina ajuste)
    {
        _context.AjustesNomina.Add(ajuste);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AjusteNomina ajuste)
    {
        _context.AjustesNomina.Update(ajuste);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var ajuste = await _context.AjustesNomina.FindAsync(id);
        if (ajuste != null)
        {
            _context.AjustesNomina.Remove(ajuste);
            await _context.SaveChangesAsync();
        }
    }
}
