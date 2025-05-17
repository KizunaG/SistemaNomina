using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    public class ExpedienteEmpleado
    {
        public int ID_Empleado { get; set; }
        public required string EstadoExpediente { get; set; } // Completo, Incompleto, En Proceso
        public DateTime? UltimaValidacion { get; set; }
    }
}
