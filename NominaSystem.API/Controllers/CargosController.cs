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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Cargo cargo)
    {
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
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
