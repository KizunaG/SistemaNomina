using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    public class Nomina
    {
        public int Id { get; set; }
        public int ID_Empleado { get; set; }
        public DateTime? PeriodoInicio { get; set; }
        public DateTime? PeriodoFin { get; set; }
        public decimal SalarioBase { get; set; }
        public decimal HorasExtras { get; set; }
        public decimal Bonificaciones { get; set; }
        public decimal Descuentos { get; set; }
        public decimal TotalPago { get; set; }
        public DateTime? FechaPago { get; set; }
        public Empleado? Empleado { get; set; }
    }
}
