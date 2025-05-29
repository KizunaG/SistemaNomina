using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    public class DocumentoEmpleado
    {
        public int Id { get; set; }
        public int ID_Empleado { get; set; }
        public required string TipoDocumento { get; set; } // Ej: DPI, Contrato, etc.
        public  required string RutaArchivo { get; set; }
        public DateTime? FechaEntrega { get; set; }
    }
}
