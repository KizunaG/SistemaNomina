using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    public class ExpedienteEmpleado
    {
        public int Id { get; set; } // ✅ Agregas esto como clave primaria

        public int ID_Empleado { get; set; }
        public string EstadoExpediente { get; set; } = "";
        public DateTime? UltimaValidacion { get; set; }
    }
}
