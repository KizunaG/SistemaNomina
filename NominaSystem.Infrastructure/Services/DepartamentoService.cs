using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
using NominaSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace NominaSystem.Infrastructure.Services;

public class DepartamentoService : IDepartamentoService
{
    private readonly ApplicationDbContext _context;

    public DepartamentoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Departamento>> GetAllAsync() =>
        await _context.Departamentos.ToListAsync();

    public async Task<Departamento?> GetByIdAsync(int id) =>
        await _context.Departamentos.FindAsync(id);

    public async Task<List<Departamento>> BuscarPorNombreAsync(string nombre) =>
        await _context.Departamentos
            .Where(d => d.NombreDepartamento.Contains(nombre))
            .ToListAsync();

    public async Task AddAsync(Departamento departamento)
    {
        _context.Departamentos.Add(departamento);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Departamento departamento)
    {
        _context.Departamentos.Update(departamento);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        // Verificar si hay empleados asignados a este departamento
        bool estaAsignado = await _context.Empleados.AnyAsync(e => e.ID_Departamento == id);
        if (estaAsignado)
        {
            // No se puede eliminar si está en uso
            return false;
        }

        var departamento = await _context.Departamentos.FindAsync(id);
        if (departamento == null)
            return false;

        _context.Departamentos.Remove(departamento);
        await _context.SaveChangesAsync();
        return true;
    }

}

