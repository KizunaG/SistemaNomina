using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Application.DTOs
{
    public class EmpleadoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Dpi { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string EstadoLaboral { get; set; } = "";
        public string Direccion { get; set; } = "";
        public DateTime? FechaIngreso { get; set; }

        public int? Id_Cargo { get; set; }  // ID del Cargo
        public int? Id_Departamento { get; set; }  // ID del Departamento
        public string NombreCargo { get; set; } = "";
        public string NombreDepartamento { get; set; } = "";
    }

}
