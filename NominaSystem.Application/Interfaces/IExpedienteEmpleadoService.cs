﻿using NominaSystem.Domain.Entities;

namespace NominaSystem.Application.Interfaces;

public interface IExpedienteEmpleadoService
{
    Task<List<ExpedienteEmpleado>> GetAllAsync();
    Task<ExpedienteEmpleado?> GetByIdAsync(int id);
    Task<bool> ValidarExpedienteCompleto(int empleadoId);
    Task AddAsync(ExpedienteEmpleado expediente);
    Task UpdateAsync(ExpedienteEmpleado expediente);
    Task DeleteAsync(int id);
}
