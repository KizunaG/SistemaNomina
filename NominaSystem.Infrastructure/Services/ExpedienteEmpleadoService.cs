using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
using NominaSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace NominaSystem.Infrastructure.Services;

public class ExpedienteEmpleadoService : IExpedienteEmpleadoService
{
    private readonly ApplicationDbContext _context;

    public ExpedienteEmpleadoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExpedienteEmpleado>> GetAllAsync() =>
        await _context.ExpedientesEmpleado.ToListAsync();
    public async Task<bool> ValidarExpedienteCompleto(int empleadoId)
    {
        // 1. Verificar documentos obligatorios entregados
        var documentosRequeridos = await _context.ConfiguracionExpedientes
            .Where(c => c.Obligatorio && c.TipoDocumento != null)
            .Select(c => c.TipoDocumento!)
            .ToListAsync();

        var documentosEntregados = await _context.DocumentosEmpleado
            .Where(d => d.ID_Empleado == empleadoId && d.TipoDocumento != null)
            .Select(d => d.TipoDocumento!)
            .ToListAsync();

        var documentosCompletos = documentosRequeridos.All(dr => documentosEntregados.Contains(dr));

        // 2. Verificar si tiene al menos una información académica
        var tieneInformacionAcademica = await _context.InformacionAcademica
            .AnyAsync(i => i.ID_Empleado == empleadoId);

        // 3. Retornar true solo si ambas condiciones se cumplen
        return documentosCompletos && tieneInformacionAcademica;
    }



    public async Task<ExpedienteEmpleado?> GetByIdAsync(int id) =>
        await _context.ExpedientesEmpleado.FindAsync(id);

    public async Task AddAsync(ExpedienteEmpleado expediente)
    {
        _context.ExpedientesEmpleado.Add(expediente);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ExpedienteEmpleado expediente)
    {
        _context.ExpedientesEmpleado.Update(expediente);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var expediente = await _context.ExpedientesEmpleado.FindAsync(id);
        if (expediente != null)
        {
            _context.ExpedientesEmpleado.Remove(expediente);
            await _context.SaveChangesAsync();
        }
    }
}
