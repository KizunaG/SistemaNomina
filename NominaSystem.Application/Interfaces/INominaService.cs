using NominaSystem.Application.DTOs;
using NominaSystem.Domain.Entities;

namespace NominaSystem.Application.Interfaces;

public interface INominaService
{
    Task<List<NominaDto>> GetAllNominasAsync();

    Task<List<Nomina>> GetAllAsync();
    Task<Nomina?> GetByIdAsync(int id);
    Task AddAsync(Nomina nomina);
    Task UpdateAsync(Nomina nomina);
    Task DeleteAsync(int id);
    Task<Nomina> ProcesarNominaAsync(Nomina nomina);
    Task<bool> ValidarAntesProcesarNominaAsync(int empleadoId);
}

