using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;

namespace NominaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InformacionAcademicaController : ControllerBase
{
    private readonly IInformacionAcademicaService _service;

    public InformacionAcademicaController(IInformacionAcademicaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var info = await _service.GetByIdAsync(id);
        return info == null ? NotFound() : Ok(info);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] InformacionAcademica info)
    {
        await _service.AddAsync(info);
        return CreatedAtAction(nameof(GetById), new { id = info.Id }, info);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] InformacionAcademica info)
    {
        if (id != info.Id) return BadRequest();
        await _service.UpdateAsync(info);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
