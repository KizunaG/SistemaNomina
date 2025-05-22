using NominaSystem.Domain.Entities;

namespace NominaSystem.Application.Interfaces;

public interface IDescuentoLegalService
{
    Task<List<DescuentoLegal>> GetAllAsync();
    Task<DescuentoLegal?> GetByIdAsync(int id);
    Task AddAsync(DescuentoLegal descuento);
    Task UpdateAsync(DescuentoLegal descuento);
    Task DeleteAsync(int id);
}
