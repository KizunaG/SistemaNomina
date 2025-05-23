using NominaSystem.Domain.Entities;
using NominaSystem.Application.Interfaces;
using NominaSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace NominaSystem.Infrastructure.Services;

public class EmpleadoService : IEmpleadoService
{
    private readonly ApplicationDbContext _context;

    public EmpleadoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Empleado>> GetAllAsync()
    {
        return await _context.Empleados.ToListAsync();
    }

    public async Task<Empleado?> GetByIdAsync(int id)
    {
        return await _context.Empleados.FindAsync(id);
    }

    public async Task AddAsync(Empleado empleado)
    {
        _context.Empleados.Add(empleado);
        await _context.SaveChangesAsync();

        // Validar expediente después de insertar y tener ID
        empleado.ExpedienteCompleto = await ValidarExpedienteCompleto(empleado.Id);
        _context.Empleados.Update(empleado);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Empleado empleado)
    {
        empleado.ExpedienteCompleto = await ValidarExpedienteCompleto(empleado.Id);
        _context.Empleados.Update(empleado);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var empleado = await _context.Empleados.FindAsync(id);
        if (empleado != null)
        {
            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();
        }
    }

    // Método privado para validar expediente completo
    private async Task<bool> ValidarExpedienteCompleto(int empleadoId)
    {
        var documentosRequeridos = await _context.ConfiguracionExpedientes
    .Where(c => c.Obligatorio && c.TipoDocumento != null)
    .Select(c => c.TipoDocumento!)
    .ToListAsync();

        var documentosEmpleado = await _context.DocumentosEmpleado
            .Where(d => d.ID_Empleado == empleadoId && d.TipoDocumento != null)
            .Select(d => d.TipoDocumento!)
            .ToListAsync();


        return documentosRequeridos.All(doc => documentosEmpleado.Contains(doc));
    }
}
