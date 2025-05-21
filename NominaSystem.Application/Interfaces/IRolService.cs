using NominaSystem.Domain.Entities;

namespace NominaSystem.Application.Interfaces;

public interface IRolService
{
    Task<List<Rol>> GetAllAsync();
    Task<Rol?> GetByIdAsync(int id);
    Task AddAsync(Rol rol);
    Task UpdateAsync(Rol rol);
    Task DeleteAsync(int id);
}

