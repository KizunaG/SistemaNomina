using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
using NominaSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace NominaSystem.Infrastructure.Services;

public class RolService : IRolService
{
    private readonly ApplicationDbContext _context;

    public RolService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Rol>> GetAllAsync() =>
        await _context.Roles.ToListAsync();

    public async Task<Rol?> GetByIdAsync(int id) =>
        await _context.Roles.FindAsync(id);

    public async Task AddAsync(Rol rol)
    {
        _context.Roles.Add(rol);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Rol rol)
    {
        _context.Roles.Update(rol);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var rol = await _context.Roles.FindAsync(id);
        if (rol != null)
        {
            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
        }
    }
}
