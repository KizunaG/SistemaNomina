using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
using NominaSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace NominaSystem.Infrastructure.Services;

public class ConfiguracionExpedienteService : IConfiguracionExpedienteService
{
    private readonly ApplicationDbContext _context;

    public ConfiguracionExpedienteService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ConfiguracionExpediente>> GetAllAsync() =>
        await _context.ConfiguracionExpedientes.ToListAsync();

    public async Task<ConfiguracionExpediente?> GetByIdAsync(int id) =>
        await _context.ConfiguracionExpedientes.FindAsync(id);

    public async Task AddAsync(ConfiguracionExpediente config)
    {
        _context.ConfiguracionExpedientes.Add(config);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ConfiguracionExpediente config)
    {
        _context.ConfiguracionExpedientes.Update(config);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var config = await _context.ConfiguracionExpedientes.FindAsync(id);
        if (config != null)
        {
            _context.ConfiguracionExpedientes.Remove(config);
            await _context.SaveChangesAsync();
        }
    }
}
