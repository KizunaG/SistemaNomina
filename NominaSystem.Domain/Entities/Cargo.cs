using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities
{
    public class Cargo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del cargo es obligatorio")]
        public required string NombreCargo { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "El salario base debe ser mayor a 0")]
        public decimal SalarioBase { get; set; }
    }
}
