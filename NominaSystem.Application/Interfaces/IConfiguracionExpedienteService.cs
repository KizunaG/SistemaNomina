using NominaSystem.Domain.Entities;

namespace NominaSystem.Application.Interfaces;

public interface IConfiguracionExpedienteService
{
    Task<List<ConfiguracionExpediente>> GetAllAsync();
    Task<ConfiguracionExpediente?> GetByIdAsync(int id);
    Task AddAsync(ConfiguracionExpediente config);
    Task UpdateAsync(ConfiguracionExpediente config);
    Task DeleteAsync(int id);
}
