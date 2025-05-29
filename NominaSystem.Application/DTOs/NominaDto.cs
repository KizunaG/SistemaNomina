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
        public string NombreEmpleado { get; set; } = "";
        public string Periodo { get; set; } = "";
        public decimal TotalPago { get; set; }
    }

}
