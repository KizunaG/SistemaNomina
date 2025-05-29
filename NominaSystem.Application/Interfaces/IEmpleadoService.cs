using NominaSystem.Application.DTOs;
using NominaSystem.Domain.Entities;

namespace NominaSystem.Application.Interfaces;

public interface IEmpleadoService
{
    Task<List<EmpleadoDto>> GetAllAsync();
    Task<EmpleadoDto?> GetByIdAsync(int id);
    Task<EmpleadoDto> AddAsync(EmpleadoDto empleadoDto);   // Cambiado para devolver EmpleadoDto con Id asignado
    Task UpdateAsync(EmpleadoDto empleadoDto);
    Task DeleteAsync(int id);
}



