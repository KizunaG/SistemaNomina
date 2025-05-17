using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    public class Departamento
    {
        public int Id { get; set; }
        public required string NombreDepartamento { get; set; }
    }
}
