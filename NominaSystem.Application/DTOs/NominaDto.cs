using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Application.DTOs
{
    public class NominaDto
    {
        public int Id { get; set; }
        public int ID_Empleado { get; set; }
        public string NombreEmpleado { get; set; } = "";
        public DateTime? PeriodoInicio { get; set; }
        public DateTime? PeriodoFin { get; set; }
        public decimal SalarioBase { get; set; }
        public decimal HorasExtras { get; set; }
        public decimal Bonificaciones { get; set; }
        public decimal Descuentos { get; set; }
        public decimal TotalPago => SalarioBase + HorasExtras + Bonificaciones - Descuentos;
        public DateTime? FechaPago { get; set; }

        // Propiedad calculada para el período
        public string Periodo => $"{PeriodoInicio?.ToString("dd/MM/yyyy")} - {PeriodoFin?.ToString("dd/MM/yyyy")}";
    }

}
