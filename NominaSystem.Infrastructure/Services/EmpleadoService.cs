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
                NombreDepartamento = e.Departamento != null ? e.Departamento.NombreDepartamento : "",
                Id_Cargo = e.ID_Cargo,
                Id_Departamento = e.ID_Departamento
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
                NombreDepartamento = e.Departamento != null ? e.Departamento.NombreDepartamento : "",
                Id_Cargo = e.ID_Cargo,
                Id_Departamento = e.ID_Departamento
            })
            .FirstOrDefaultAsync();

        return empleado;
    }

    public async Task<EmpleadoDto> AddAsync(EmpleadoDto empleadoDto)
    {
        // Validar que los Ids existan si son diferentes de null
        if (empleadoDto.Id_Cargo.HasValue)
        {
            var cargo = await _context.Cargos.FirstOrDefaultAsync(c => c.Id == empleadoDto.Id_Cargo.Value);
            if (cargo == null) throw new Exception("El Cargo seleccionado no existe.");
        }

        if (empleadoDto.Id_Departamento.HasValue)
        {
            var departamento = await _context.Departamentos.FirstOrDefaultAsync(d => d.Id == empleadoDto.Id_Departamento.Value);
            if (departamento == null) throw new Exception("El Departamento seleccionado no existe.");
        }

        var empleado = new Empleado
        {
            Nombre = empleadoDto.Nombre,
            DPI = empleadoDto.Dpi,
            Telefono = empleadoDto.Telefono,
            EstadoLaboral = empleadoDto.EstadoLaboral,
            Direccion = empleadoDto.Direccion,
            FechaIngreso = empleadoDto.FechaIngreso,
            ID_Cargo = empleadoDto.Id_Cargo,
            ID_Departamento = empleadoDto.Id_Departamento
        };

        _context.Empleados.Add(empleado);
        await _context.SaveChangesAsync();

        // Asignar el Id generado al DTO
        empleadoDto.Id = empleado.Id;

        return empleadoDto;
    }

    public async Task UpdateAsync(EmpleadoDto empleadoDto)
    {
        var empleado = await _context.Empleados.FindAsync(empleadoDto.Id);
        if (empleado == null) throw new KeyNotFoundException($"Empleado con Id {empleadoDto.Id} no encontrado.");

        // Buscar Cargo si Id_Cargo tiene valor
        if (empleadoDto.Id_Cargo.HasValue)
        {
            var cargo = await _context.Cargos.FindAsync(empleadoDto.Id_Cargo.Value);
            if (cargo == null) throw new Exception("El Cargo seleccionado no existe.");
            empleado.ID_Cargo = cargo.Id;
        }
        else
        {
            empleado.ID_Cargo = null;
        }

        // Buscar Departamento si Id_Departamento tiene valor
        if (empleadoDto.Id_Departamento.HasValue)
        {
            var departamento = await _context.Departamentos.FindAsync(empleadoDto.Id_Departamento.Value);
            if (departamento == null) throw new Exception("El Departamento seleccionado no existe.");
            empleado.ID_Departamento = departamento.Id;
        }
        else
        {
            empleado.ID_Departamento = null;
        }

        empleado.Nombre = empleadoDto.Nombre;
        empleado.DPI = empleadoDto.Dpi;
        empleado.Telefono = empleadoDto.Telefono;
        empleado.EstadoLaboral = empleadoDto.EstadoLaboral;
        empleado.Direccion = empleadoDto.Direccion;
        empleado.FechaIngreso = empleadoDto.FechaIngreso;

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


