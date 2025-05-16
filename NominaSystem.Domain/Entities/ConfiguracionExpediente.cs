using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    internal class ConfiguracionExpediente
    {
        public int Id { get; set; }
        public string TipoDocumento { get; set; }
        public bool Obligatorio { get; set; } = true;
    }
}
