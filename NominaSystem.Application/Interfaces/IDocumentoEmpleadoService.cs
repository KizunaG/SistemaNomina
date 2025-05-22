using NominaSystem.Domain.Entities;

namespace NominaSystem.Application.Interfaces;

public interface IDocumentoEmpleadoService
{
    Task<List<DocumentoEmpleado>> GetAllAsync();
    Task<DocumentoEmpleado?> GetByIdAsync(int id);
    Task AddAsync(DocumentoEmpleado documento);
    Task UpdateAsync(DocumentoEmpleado documento);
    Task DeleteAsync(int id);
}
