using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    public class Rol
    {
        public int Id { get; set; }
        public required string NombreRol { get; set; }
    }
}
