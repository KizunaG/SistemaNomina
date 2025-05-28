using NominaSystem.Domain.Entities;
using NominaSystem.Application.Interfaces;
using NominaSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NominaSystem.Application.DTOs;

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

    public async Task<Empleado?> GetByIdAsync(int id)
    {
        return await _context.Empleados.FindAsync(id);
    }

    public async Task AddAsync(Empleado empleado)
    {
        // Buscar o crear Cargo por nombre
        Cargo? cargo = null;
        if (!string.IsNullOrWhiteSpace(empleado.NombreCargo))
        {
            cargo = await _context.Cargos.FirstOrDefaultAsync(c => c.NombreCargo == empleado.NombreCargo);
            if (cargo == null)
            {
                cargo = new Cargo { NombreCargo = empleado.NombreCargo };
                _context.Cargos.Add(cargo);
                await _context.SaveChangesAsync();
            }
        }

        // Buscar o crear Departamento por nombre
        Departamento? departamento = null;
        if (!string.IsNullOrWhiteSpace(empleado.NombreDepartamento))
        {
            departamento = await _context.Departamentos.FirstOrDefaultAsync(d => d.NombreDepartamento == empleado.NombreDepartamento);
            if (departamento == null)
            {
                departamento = new Departamento { NombreDepartamento = empleado.NombreDepartamento };
                _context.Departamentos.Add(departamento);
                await _context.SaveChangesAsync();
            }
        }

        // Asignar IDs al empleado
        empleado.ID_Cargo = cargo?.Id;
        empleado.ID_Departamento = departamento?.Id;

        // Limpiar propiedades de navegación para evitar problemas al insertar
        empleado.Cargo = null;
        empleado.Departamento = null;

        _context.Empleados.Add(empleado);
        await _context.SaveChangesAsync();

        // Validar expediente después de insertar y tener ID
        empleado.ExpedienteCompleto = await ValidarExpedienteCompleto(empleado.Id);
        _context.Empleados.Update(empleado);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Empleado empleado)
    {
        // Buscar o crear Cargo por nombre
        Cargo? cargo = null;
        if (!string.IsNullOrWhiteSpace(empleado.NombreCargo))
        {
            cargo = await _context.Cargos.FirstOrDefaultAsync(c => c.NombreCargo == empleado.NombreCargo);
            if (cargo == null)
            {
                cargo = new Cargo { NombreCargo = empleado.NombreCargo };
                _context.Cargos.Add(cargo);
                await _context.SaveChangesAsync();
            }
        }

        // Buscar o crear Departamento por nombre
        Departamento? departamento = null;
        if (!string.IsNullOrWhiteSpace(empleado.NombreDepartamento))
        {
            departamento = await _context.Departamentos.FirstOrDefaultAsync(d => d.NombreDepartamento == empleado.NombreDepartamento);
            if (departamento == null)
            {
                departamento = new Departamento { NombreDepartamento = empleado.NombreDepartamento };
                _context.Departamentos.Add(departamento);
                await _context.SaveChangesAsync();
            }
        }

        // Asignar IDs al empleado
        empleado.ID_Cargo = cargo?.Id;
        empleado.ID_Departamento = departamento?.Id;

        // Limpiar propiedades de navegación para evitar problemas al actualizar
        empleado.Cargo = null;
        empleado.Departamento = null;

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

