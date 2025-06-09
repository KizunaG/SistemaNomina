using System.ComponentModel.DataAnnotations;

namespace NominaSystem.UI.Models
{
    public class NominaDto : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El empleado es obligatorio.")]
        public int ID_Empleado { get; set; }
        public string NombreEmpleado { get; set; } = "";

        [Required(ErrorMessage = "El período de inicio es obligatorio.")]
        public DateTime? PeriodoInicio { get; set; }

        [Required(ErrorMessage = "El período de fin es obligatorio.")]
        public DateTime? PeriodoFin { get; set; }

        [Required(ErrorMessage = "El salario base es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El salario base debe ser positivo.")]
        public decimal SalarioBase { get; set; }
        public decimal HorasExtras { get; set; }
        public decimal Bonificaciones { get; set; }
        public decimal Descuentos { get; set; }
        public decimal IGSS { get; set; }
        public decimal TotalPago => SalarioBase + HorasExtras + Bonificaciones - Descuentos-IGSS;
        public DateTime? FechaPago { get; set; }

        // Propiedad calculada para el período
        public string Periodo => $"{PeriodoInicio?.ToString("dd/MM/yyyy")} - {PeriodoFin?.ToString("dd/MM/yyyy")}";
  
            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PeriodoInicio.HasValue && PeriodoFin.HasValue)
            {
                if (PeriodoInicio.Value >= PeriodoFin.Value)
                {
                    yield return new ValidationResult(
                        "La fecha de periodo inicio debe ser anterior a la fecha de periodo fin.",
                        new[] { nameof(PeriodoInicio), nameof(PeriodoFin) });
                }
            }
        }

    }
}
