using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    internal class DescuentoLegal
    {
        public int Id { get; set; }
        public string NombreDescuento { get; set; }
        public decimal Porcentaje { get; set; }
        public string? Descripcion { get; set; }
    }
}
