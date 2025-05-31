using Microsoft.AspNetCore.Mvc;
using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;

namespace NominaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentoEmpleadoController : ControllerBase
{
    private readonly IDocumentoEmpleadoService _service;

    public DocumentoEmpleadoController(IDocumentoEmpleadoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var documento = await _service.GetByIdAsync(id);
        return documento == null ? NotFound() : Ok(documento);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DocumentoEmpleado documento)
    {
        await _service.AddAsync(documento);
        return CreatedAtAction(nameof(GetById), new { id = documento.Id }, documento);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DocumentoEmpleado documento)
    {
        if (id != documento.Id) return BadRequest();
        await _service.UpdateAsync(documento);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
    [HttpGet("por-empleado/{idEmpleado}")]
    public async Task<IActionResult> GetPorEmpleado(int idEmpleado)
    {
        var documentos = await _service.GetAllAsync();
        var filtrados = documentos.Where(d => d.ID_Empleado == idEmpleado).ToList();

        return Ok(filtrados);
    }

}
