namespace NominaSystem.UI.Models
{
    public class NominaDto
    {
        public int Id { get; set; }
        public string NombreEmpleado { get; set; } = "";
        public string Periodo { get; set; } = "";
        public decimal TotalPago { get; set; }
    }
}
