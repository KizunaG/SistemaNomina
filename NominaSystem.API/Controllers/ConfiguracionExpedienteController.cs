using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;

namespace NominaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConfiguracionExpedienteController : ControllerBase
{
    private readonly IConfiguracionExpedienteService _service;

    public ConfiguracionExpedienteController(IConfiguracionExpedienteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var config = await _service.GetByIdAsync(id);
        return config == null ? NotFound() : Ok(config);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ConfiguracionExpediente config)
    {
        await _service.AddAsync(config);
        return CreatedAtAction(nameof(GetById), new { id = config.Id }, config);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ConfiguracionExpediente config)
    {
        if (id != config.Id) return BadRequest();
        await _service.UpdateAsync(config);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
