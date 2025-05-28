using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using NominaSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NominaSystem.Application.DTOs;

namespace NominaSystem.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmpleadosController : ControllerBase
{
    private readonly IEmpleadoService _service;
    private readonly ApplicationDbContext _context;

    public EmpleadosController(IEmpleadoService service, ApplicationDbContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpGet("filtrar")]
    public async Task<IActionResult> Filtrar([FromQuery] string? busqueda, [FromQuery] int pagina = 1, [FromQuery] int tamanoPagina = 10)
    {
        var query = _context.Empleados.AsQueryable();

        if (!string.IsNullOrWhiteSpace(busqueda))
        {
            query = query.Where(e => e.Nombre.Contains(busqueda) || e.DPI.Contains(busqueda));
        }

        var totalRegistros = await query.CountAsync();

        var empleados = await query
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync();

        return Ok(new
        {
            Datos = empleados,
            Total = totalRegistros
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var empleados = await _context.Empleados
            .Include(e => e.Cargo)         // Incluye la relación con Cargo
            .Include(e => e.Departamento)  // Incluye la relación con Departamento
            .Select(e => new EmpleadoDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Dpi = e.DPI,
                Telefono = e.Telefono,
                EstadoLaboral = e.EstadoLaboral,
                Direccion = e.Direccion,
                FechaIngreso = e.FechaIngreso ?? DateTime.MinValue,
                NombreCargo = e.Cargo != null ? e.Cargo.NombreCargo : "",  // Acceder al nombre de Cargo
                NombreDepartamento = e.Departamento != null ? e.Departamento.NombreDepartamento : ""  // Acceder al nombre de Departamento
            })
            .ToListAsync();

        return Ok(empleados);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
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

        if (empleado == null) return NotFound();

        return Ok(empleado);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmpleadoDto empleadoDto)
    {
        // Buscar o crear Cargo
        var cargo = await _context.Cargos
            .FirstOrDefaultAsync(c => c.NombreCargo == empleadoDto.NombreCargo);

        if (cargo == null)
        {
            cargo = new Cargo { NombreCargo = empleadoDto.NombreCargo };
            _context.Cargos.Add(cargo);
            await _context.SaveChangesAsync();
        }

        // Buscar o crear Departamento
        var departamento = await _context.Departamentos
            .FirstOrDefaultAsync(d => d.NombreDepartamento == empleadoDto.NombreDepartamento);

        if (departamento == null)
        {
            departamento = new Departamento { NombreDepartamento = empleadoDto.NombreDepartamento };
            _context.Departamentos.Add(departamento);
            await _context.SaveChangesAsync();
        }

        // Crear el empleado usando los IDs de cargo y departamento
        var empleado = new Empleado
        {
            Nombre = empleadoDto.Nombre,
            DPI = empleadoDto.Dpi,
            Telefono = empleadoDto.Telefono,
            EstadoLaboral = empleadoDto.EstadoLaboral,
            Direccion = empleadoDto.Direccion,
            FechaIngreso = empleadoDto.FechaIngreso,
            ID_Cargo = cargo.Id,
            ID_Departamento = departamento.Id
        };

        _context.Empleados.Add(empleado);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = empleado.Id }, empleado);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Empleado empleado)
    {
        if (id != empleado.Id) return BadRequest("ID no coincide");
        await _service.UpdateAsync(empleado);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
