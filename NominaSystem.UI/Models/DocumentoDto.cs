public class DocumentoDto
{
    public int Id { get; set; }
    public int ID_Empleado { get; set; }  // Asegúrate que este campo esté para la relación
    public string? Nombre { get; set; }
    public required string TipoDocumento { get; set; }  // Requerido
    public required string RutaArchivo { get; set; }    // Requerido
    public DateTime? FechaEntrega { get; set; }
}
