using NominaSystem.Application.DTOs;
using NominaSystem.Domain.Entities;

namespace NominaSystem.Application.Interfaces;

public interface IEmpleadoService
{
    Task<List<EmpleadoDto>> GetAllAsync();
    Task<EmpleadoDto?> GetByIdAsync(int id);
    Task AddAsync(EmpleadoDto empleadoDto);   // Cambiado a EmpleadoDto
    Task UpdateAsync(EmpleadoDto empleadoDto); // Cambiado a EmpleadoDto
    Task DeleteAsync(int id);
}


