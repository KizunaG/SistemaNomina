using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    internal class AjusteNomina
    {
        public int Id { get; set; }
        public int ID_Nomina { get; set; }
        public string TipoAjuste { get; set; } // Bonificacion o Descuento
        public decimal Monto { get; set; }
        public string Motivo { get; set; }
        public DateTime? FechaAjuste { get; set; }
    }
}
