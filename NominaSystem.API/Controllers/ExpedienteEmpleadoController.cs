using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;

namespace NominaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpedienteEmpleadoController : ControllerBase
{
    private readonly IExpedienteEmpleadoService _service;

    public ExpedienteEmpleadoController(IExpedienteEmpleadoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var expediente = await _service.GetByIdAsync(id);
        return expediente == null ? NotFound() : Ok(expediente);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ExpedienteEmpleado expediente)
    {
        await _service.AddAsync(expediente);
        return CreatedAtAction(nameof(GetById), new { id = expediente.Id }, expediente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ExpedienteEmpleado expediente)
    {
        if (id != expediente.Id) return BadRequest();
        await _service.UpdateAsync(expediente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
