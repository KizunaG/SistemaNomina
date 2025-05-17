using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    public class Cargo
    {
        public int Id { get; set; }
        public required string NombreCargo { get; set; }
        public decimal SalarioBase { get; set; }
    }
}
