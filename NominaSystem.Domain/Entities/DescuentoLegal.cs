using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    public class DescuentoLegal
    {
        public int Id { get; set; }
        public required string NombreDescuento { get; set; }
        public decimal Porcentaje { get; set; }
        public string? Descripcion { get; set; }
    }
}
