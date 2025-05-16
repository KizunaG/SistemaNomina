using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    internal class InformacionAcademica
    {
        public int Id { get; set; }
        public int ID_Empleado { get; set; }
        public string Titulo { get; set; }
        public string Institucion { get; set; }
        public DateTime? FechaGraduacion { get; set; }
    }
}
