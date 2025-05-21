using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;

namespace NominaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartamentosController : ControllerBase
{
    private readonly IDepartamentoService _service;

    public DepartamentosController(IDepartamentoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var departamento = await _service.GetByIdAsync(id);
        return departamento == null ? NotFound() : Ok(departamento);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Departamento departamento)
    {
        await _service.AddAsync(departamento);
        return CreatedAtAction(nameof(GetById), new { id = departamento.Id }, departamento);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Departamento departamento)
    {
        if (id != departamento.Id) return BadRequest();
        await _service.UpdateAsync(departamento);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
