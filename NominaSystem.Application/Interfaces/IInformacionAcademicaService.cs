using NominaSystem.Domain.Entities;

namespace NominaSystem.Application.Interfaces;

public interface IInformacionAcademicaService
{
    Task<List<InformacionAcademica>> GetAllAsync();
    Task<InformacionAcademica?> GetByIdAsync(int id);
    Task AddAsync(InformacionAcademica info);
    Task UpdateAsync(InformacionAcademica info);
    Task DeleteAsync(int id);

    Task<List<InformacionAcademica>> GetByEmpleadoIdAsync(int empleadoId);
}
