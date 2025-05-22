using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;

namespace NominaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DescuentoLegalController : ControllerBase
{
    private readonly IDescuentoLegalService _service;

    public DescuentoLegalController(IDescuentoLegalService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var descuento = await _service.GetByIdAsync(id);
        return descuento == null ? NotFound() : Ok(descuento);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DescuentoLegal descuento)
    {
        await _service.AddAsync(descuento);
        return CreatedAtAction(nameof(GetById), new { id = descuento.Id }, descuento);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DescuentoLegal descuento)
    {
        if (id != descuento.Id) return BadRequest();
        await _service.UpdateAsync(descuento);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
