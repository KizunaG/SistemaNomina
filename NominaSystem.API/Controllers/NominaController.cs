using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;

namespace NominaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NominaController : ControllerBase
{
    private readonly INominaService _service;

    public NominaController(INominaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var nomina = await _service.GetByIdAsync(id);
        return nomina == null ? NotFound() : Ok(nomina);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Nomina nomina)
    {
        await _service.AddAsync(nomina);
        return CreatedAtAction(nameof(GetById), new { id = nomina.Id }, nomina);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Nomina nomina)
    {
        if (id != nomina.Id) return BadRequest();
        await _service.UpdateAsync(nomina);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
