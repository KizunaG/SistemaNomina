using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    public class Nomina
    {
        public int Id { get; set; }
        // Usamos el atributo [Column] para mapear la propiedad a la columna con nombre diferente
        [Column("ID_Empleado")]
        public int EmpleadoId { get; set; }  // Esta es la propiedad en la entidad
        public Empleado Empleado { get; set; }  // Relación con Empleado
        public DateTime? PeriodoInicio { get; set; }
        public DateTime? PeriodoFin { get; set; }
        public decimal SalarioBase { get; set; }
        public decimal HorasExtras { get; set; }
        public decimal Bonificaciones { get; set; }
        public decimal Descuentos { get; set; }
        public decimal TotalPago { get; set; }
        public DateTime? FechaPago { get; set; }
    }
}
