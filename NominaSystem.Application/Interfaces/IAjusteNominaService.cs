using NominaSystem.Domain.Entities;

namespace NominaSystem.Application.Interfaces;

public interface IAjusteNominaService
{
    Task<List<AjusteNomina>> GetAllAsync();
    Task<AjusteNomina?> GetByIdAsync(int id);
    Task AddAsync(AjusteNomina ajuste);
    Task UpdateAsync(AjusteNomina ajuste);
    Task DeleteAsync(int id);
}
