using NominaSystem.Domain.Entities;

namespace NominaSystem.Application.Interfaces;

public interface IDepartamentoService
{
    Task<List<Departamento>> GetAllAsync();
    Task<Departamento?> GetByIdAsync(int id);
    Task AddAsync(Departamento departamento);
    Task UpdateAsync(Departamento departamento);
    Task DeleteAsync(int id);
}
