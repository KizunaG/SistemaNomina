using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using NominaSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
        var empleados = await _service.GetAllAsync();
        return Ok(empleados);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var empleado = await _service.GetByIdAsync(id);
        if (empleado == null) return NotFound();
        return Ok(empleado);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Empleado empleado)
    {
        await _service.AddAsync(empleado);
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
