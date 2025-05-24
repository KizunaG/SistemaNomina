using NominaSystem.Domain.Entities;

namespace NominaSystem.Application.Interfaces;

public interface IDetalleDescuentoNominaService
{
    Task<List<DetalleDescuentoNomina>> GetAllAsync();
    Task<DetalleDescuentoNomina?> GetByIdAsync(int id);
    Task AddAsync(DetalleDescuentoNomina detalle);
    Task UpdateAsync(DetalleDescuentoNomina detalle);
    Task DeleteAsync(int id);
}
