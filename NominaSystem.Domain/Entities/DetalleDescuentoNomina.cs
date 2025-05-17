using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    public class DetalleDescuentoNomina
    {
        public int Id { get; set; }
        public int ID_Nomina { get; set; }
        public int ID_Descuento { get; set; }
        public decimal Monto { get; set; }
    }
}
