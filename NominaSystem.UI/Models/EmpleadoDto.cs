namespace NominaSystem.UI.Models;

public class EmpleadoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Dpi { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string EstadoLaboral { get; set; } = string.Empty;
    public string Direccion { get; set; } = "";
    public DateTime FechaIngreso { get; set; }
    public string? CargoNombre { get; set; }  // ← nombre del cargo
    public string? DepartamentoNombre { get; set; } // ← nombre del departamento
}
