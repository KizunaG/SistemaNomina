using NominaSystem.Application.Interfaces;
using NominaSystem.Domain.Entities;
using NominaSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace NominaSystem.Infrastructure.Services;

public class DocumentoEmpleadoService : IDocumentoEmpleadoService
{
    private readonly ApplicationDbContext _context;

    public DocumentoEmpleadoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DocumentoEmpleado>> GetAllAsync() =>
        await _context.DocumentosEmpleado.ToListAsync();

    public async Task<DocumentoEmpleado?> GetByIdAsync(int id) =>
        await _context.DocumentosEmpleado.FindAsync(id);

    public async Task AddAsync(DocumentoEmpleado documento)
    {
        _context.DocumentosEmpleado.Add(documento);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(DocumentoEmpleado documento)
    {
        var existente = await _context.DocumentosEmpleado.FindAsync(documento.Id);
        if (existente != null)
        {
            existente.TipoDocumento = documento.TipoDocumento;
            existente.RutaArchivo = documento.RutaArchivo;
            existente.FechaEntrega = documento.FechaEntrega;
            existente.Nombre = documento.Nombre;
            existente.ID_Empleado = documento.ID_Empleado;

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var documento = await _context.DocumentosEmpleado.FindAsync(id);
        if (documento != null)
        {
            _context.DocumentosEmpleado.Remove(documento);
            await _context.SaveChangesAsync();
        }
    }
}
