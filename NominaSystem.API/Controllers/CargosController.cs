using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;

namespace NominaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CargosController : ControllerBase
{
    private readonly ICargoService _service;

    public CargosController(ICargoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var cargo = await _service.GetByIdAsync(id);
        return cargo == null ? NotFound() : Ok(cargo);
    }

    // Nuevo endpoint para búsqueda por nombre parcial o completo
    [HttpGet("buscar")]
    public async Task<IActionResult> BuscarPorNombre([FromQuery] string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return BadRequest("Debe proporcionar un nombre para la búsqueda.");

        var resultados = await _service.BuscarPorNombreAsync(nombre);
        return Ok(resultados);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Cargo cargo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _service.AddAsync(cargo);
        return CreatedAtAction(nameof(GetById), new { id = cargo.Id }, cargo);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Cargo cargo)
    {
        if (id != cargo.Id) return BadRequest();
        await _service.UpdateAsync(cargo);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCargo(int id)
    {
        try
        {
            bool eliminado = await _service.DeleteAsync(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }


}

