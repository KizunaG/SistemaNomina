using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    public class AjusteNomina
    {
        public int Id { get; set; }
        public int ID_Nomina { get; set; }
        public required string TipoAjuste { get; set; } // Bonificacion o Descuento
        public decimal Monto { get; set; }
        public required string Motivo { get; set; }
        public DateTime? FechaAjuste { get; set; }
    }
}
