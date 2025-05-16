using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    internal class ExpedienteEmpleado
    {
        public int ID_Empleado { get; set; }
        public string EstadoExpediente { get; set; } // Completo, Incompleto, En Proceso
        public DateTime? UltimaValidacion { get; set; }
    }
}
