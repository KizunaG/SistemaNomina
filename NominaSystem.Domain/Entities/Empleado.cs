using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities;

public class Empleado
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string DPI { get; set; }
    public string EstadoLaboral { get; set; }
    public DateTime? FechaIngreso { get; set; }

    public int? ID_Cargo { get; set; }
    public int? ID_Departamento { get; set; }
}
