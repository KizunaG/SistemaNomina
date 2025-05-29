using NominaSystem.Application.DTOs;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
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

    public async Task<List<EmpleadoDto>> GetAllAsync()
    {
        return await _context.Empleados
            .Include(e => e.Cargo)
            .Include(e => e.Departamento)
            .Select(e => new EmpleadoDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Dpi = e.DPI,
                Telefono = e.Telefono,
                EstadoLaboral = e.EstadoLaboral,
                Direccion = e.Direccion,
                FechaIngreso = e.FechaIngreso ?? default,
                NombreCargo = e.Cargo != null ? e.Cargo.NombreCargo : "",
                NombreDepartamento = e.Departamento != null ? e.Departamento.NombreDepartamento : ""
            })
            .ToListAsync();
    }

    public async Task<EmpleadoDto?> GetByIdAsync(int id)
    {
        var empleado = await _context.Empleados
            .Include(e => e.Cargo)
            .Include(e => e.Departamento)
            .Where(e => e.Id == id)
            .Select(e => new EmpleadoDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Dpi = e.DPI,
                Telefono = e.Telefono,
                EstadoLaboral = e.EstadoLaboral,
                Direccion = e.Direccion,
                FechaIngreso = e.FechaIngreso ?? default,
                NombreCargo = e.Cargo != null ? e.Cargo.NombreCargo : "",
                NombreDepartamento = e.Departamento != null ? e.Departamento.NombreDepartamento : ""
            })
            .FirstOrDefaultAsync();

        return empleado;
    }

    public async Task AddAsync(EmpleadoDto empleadoDto)
    {
        // Buscar o crear Cargo
        var cargo = await _context.Cargos.FirstOrDefaultAsync(c => c.NombreCargo == empleadoDto.NombreCargo);
        if (cargo == null && !string.IsNullOrWhiteSpace(empleadoDto.NombreCargo))
        {
            cargo = new Cargo { NombreCargo = empleadoDto.NombreCargo };
            _context.Cargos.Add(cargo);
            await _context.SaveChangesAsync();
        }

        // Buscar o crear Departamento
        var departamento = await _context.Departamentos.FirstOrDefaultAsync(d => d.NombreDepartamento == empleadoDto.NombreDepartamento);
        if (departamento == null && !string.IsNullOrWhiteSpace(empleadoDto.NombreDepartamento))
        {
            departamento = new Departamento { NombreDepartamento = empleadoDto.NombreDepartamento };
            _context.Departamentos.Add(departamento);
            await _context.SaveChangesAsync();
        }

        var empleado = new Empleado
        {
            Nombre = empleadoDto.Nombre,
            DPI = empleadoDto.Dpi,
            Telefono = empleadoDto.Telefono,
            EstadoLaboral = empleadoDto.EstadoLaboral,
            Direccion = empleadoDto.Direccion,
            FechaIngreso = empleadoDto.FechaIngreso,
            ID_Cargo = cargo?.Id,
            ID_Departamento = departamento?.Id
        };

        _context.Empleados.Add(empleado);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EmpleadoDto empleadoDto)
    {
        var empleado = await _context.Empleados.FindAsync(empleadoDto.Id);
        if (empleado == null) throw new KeyNotFoundException($"Empleado con Id {empleadoDto.Id} no encontrado.");

        // Buscar o crear Cargo
        var cargo = await _context.Cargos.FirstOrDefaultAsync(c => c.NombreCargo == empleadoDto.NombreCargo);
        if (cargo == null && !string.IsNullOrWhiteSpace(empleadoDto.NombreCargo))
        {
            cargo = new Cargo { NombreCargo = empleadoDto.NombreCargo };
            _context.Cargos.Add(cargo);
            await _context.SaveChangesAsync();
        }

        // Buscar o crear Departamento
        var departamento = await _context.Departamentos.FirstOrDefaultAsync(d => d.NombreDepartamento == empleadoDto.NombreDepartamento);
        if (departamento == null && !string.IsNullOrWhiteSpace(empleadoDto.NombreDepartamento))
        {
            departamento = new Departamento { NombreDepartamento = empleadoDto.NombreDepartamento };
            _context.Departamentos.Add(departamento);
            await _context.SaveChangesAsync();
        }

        empleado.Nombre = empleadoDto.Nombre;
        empleado.DPI = empleadoDto.Dpi;
        empleado.Telefono = empleadoDto.Telefono;
        empleado.EstadoLaboral = empleadoDto.EstadoLaboral;
        empleado.Direccion = empleadoDto.Direccion;
        empleado.FechaIngreso = empleadoDto.FechaIngreso;
        empleado.ID_Cargo = cargo?.Id;
        empleado.ID_Departamento = departamento?.Id;

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
}

