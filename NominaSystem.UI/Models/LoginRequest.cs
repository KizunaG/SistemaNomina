using System.ComponentModel.DataAnnotations;

namespace NominaSystem.UI.Models;

public class LoginRequest
{
    [Required(ErrorMessage = "Correo obligatorio")]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contraseña obligatoria")]
    public string Contrasena { get; set; } = string.Empty;
}
