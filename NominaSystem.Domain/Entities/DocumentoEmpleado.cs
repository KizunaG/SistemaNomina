using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    internal class DocumentoEmpleado
    {
        public int Id { get; set; }
        public int ID_Empleado { get; set; }
        public string TipoDocumento { get; set; } // Ej: DPI, Contrato, etc.
        public string RutaArchivo { get; set; }
        public DateTime? FechaEntrega { get; set; }
    }
}
