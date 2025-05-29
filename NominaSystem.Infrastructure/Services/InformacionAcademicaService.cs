using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
using NominaSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace NominaSystem.Infrastructure.Services;

public class InformacionAcademicaService : IInformacionAcademicaService
{
    private readonly ApplicationDbContext _context;

    public InformacionAcademicaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<InformacionAcademica>> GetAllAsync() =>
        await _context.InformacionAcademica.ToListAsync();

    public async Task<InformacionAcademica?> GetByIdAsync(int id) =>
        await _context.InformacionAcademica.FindAsync(id);

    public async Task AddAsync(InformacionAcademica info)
    {
        _context.InformacionAcademica.Add(info);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(InformacionAcademica info)
    {
        _context.InformacionAcademica.Update(info);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var info = await _context.InformacionAcademica.FindAsync(id);
        if (info != null)
        {
            _context.InformacionAcademica.Remove(info);
            await _context.SaveChangesAsync();
        }
    }
}
