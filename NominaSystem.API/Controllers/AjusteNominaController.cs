using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;

namespace NominaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AjusteNominaController : ControllerBase
{
    private readonly IAjusteNominaService _service;

    public AjusteNominaController(IAjusteNominaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ajuste = await _service.GetByIdAsync(id);
        return ajuste == null ? NotFound() : Ok(ajuste);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AjusteNomina ajuste)
    {
        await _service.AddAsync(ajuste);
        return CreatedAtAction(nameof(GetById), new { id = ajuste.Id }, ajuste);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AjusteNomina ajuste)
    {
        if (id != ajuste.Id) return BadRequest();
        await _service.UpdateAsync(ajuste);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
