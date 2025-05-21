using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;

namespace NominaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRolService _service;

    public RolesController(IRolService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var rol = await _service.GetByIdAsync(id);
        return rol == null ? NotFound() : Ok(rol);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Rol rol)
    {
        await _service.AddAsync(rol);
        return CreatedAtAction(nameof(GetById), new { id = rol.Id }, rol);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Rol rol)
    {
        if (id != rol.Id) return BadRequest();
        await _service.UpdateAsync(rol);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
