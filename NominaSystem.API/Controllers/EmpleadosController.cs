using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using NominaSystem.Application.DTOs;

namespace NominaSystem.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmpleadosController : ControllerBase
{
    private readonly IEmpleadoService _service;

    public EmpleadosController(IEmpleadoService service)
    {
        _service = service;
    }

    [HttpGet("filtrar")]
    public async Task<IActionResult> Filtrar([FromQuery] string? busqueda, [FromQuery] int pagina = 1, [FromQuery] int tamanoPagina = 10)
    {
        var empleados = await _service.GetAllAsync();

        var filtered = empleados.Where(e =>
            string.IsNullOrWhiteSpace(busqueda) ||
            e.Nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase) ||
            e.Dpi.Contains(busqueda, StringComparison.OrdinalIgnoreCase));

        var total = filtered.Count();

        var paged = filtered
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToList();

        return Ok(new { Datos = paged, Total = total });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var empleadosDto = await _service.GetAllAsync();
        return Ok(empleadosDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var empleado = await _service.GetByIdAsync(id);
        if (empleado == null) return NotFound();
        return Ok(empleado);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmpleadoDto empleadoDto)
    {
        // Pasa el DTO directamente al servicio que se encargará de crear la entidad y mapear cargos/departamentos
        await _service.AddAsync(empleadoDto);

        // Para devolver un CreatedAtAction válido, obtén el empleado recién creado
        var empleadoCreado = await _service.GetByIdAsync(empleadoDto.Id);

        return CreatedAtAction(nameof(GetById), new { id = empleadoCreado?.Id }, empleadoCreado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EmpleadoDto empleadoDto)
    {
        if (id != empleadoDto.Id) return BadRequest("ID no coincide");

        await _service.UpdateAsync(empleadoDto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}



