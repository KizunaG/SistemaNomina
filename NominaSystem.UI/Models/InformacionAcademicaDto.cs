public class InformacionAcademicaDto
{
    public int Id { get; set; }
    public int ID_Empleado { get; set; }  // Relación con empleado
    public required string Titulo { get; set; }
    public string Institucion { get; set; } = string.Empty;
    public DateTime? FechaGraduacion { get; set; }
}
