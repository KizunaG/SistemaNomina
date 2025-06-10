using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
using NominaSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace NominaSystem.Infrastructure.Services;

public class CargoService : ICargoService
{
    private readonly ApplicationDbContext _context;

    public CargoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cargo>> GetAllAsync() =>
        await _context.Cargos.ToListAsync();

    public async Task<Cargo?> GetByIdAsync(int id) =>
        await _context.Cargos.FindAsync(id);

    public async Task AddAsync(Cargo cargo)
    {
        _context.Cargos.Add(cargo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Cargo cargo)
    {
        _context.Cargos.Update(cargo);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cargo = await _context.Cargos.FindAsync(id);
        if (cargo == null)
            return false;

        // ❌ Validación de asignación
        var estaAsignado = await _context.Empleados.AnyAsync(e => e.ID_Cargo == id);
        if (estaAsignado)
            throw new InvalidOperationException("El cargo está asignado a uno o más empleados.");

        _context.Cargos.Remove(cargo);
        await _context.SaveChangesAsync();

        return true;
    }



    // Nuevo método para buscar cargos por nombre
    public async Task<List<Cargo>> BuscarPorNombreAsync(string nombre)
    {
        return await _context.Cargos
            .Where(c => c.NombreCargo.Contains(nombre))
            .ToListAsync();
    }
}

