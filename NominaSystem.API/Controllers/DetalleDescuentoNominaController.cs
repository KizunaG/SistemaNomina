using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;

namespace NominaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetalleDescuentoNominaController : ControllerBase
{
    private readonly IDetalleDescuentoNominaService _service;

    public DetalleDescuentoNominaController(IDetalleDescuentoNominaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var detalle = await _service.GetByIdAsync(id);
        return detalle == null ? NotFound() : Ok(detalle);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DetalleDescuentoNomina detalle)
    {
        await _service.AddAsync(detalle);
        return CreatedAtAction(nameof(GetById), new { id = detalle.Id }, detalle);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DetalleDescuentoNomina detalle)
    {
        if (id != detalle.Id) return BadRequest();
        await _service.UpdateAsync(detalle);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
