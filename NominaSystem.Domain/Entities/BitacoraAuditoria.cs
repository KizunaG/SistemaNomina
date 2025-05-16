using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    internal class BitacoraAuditoria
    {
        public int Id { get; set; }
        public int ID_Usuario { get; set; }
        public string Accion { get; set; }
        public string TablaAfectada { get; set; }
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public string Detalles { get; set; }
    }
}
