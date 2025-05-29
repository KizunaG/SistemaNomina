using System.ComponentModel.DataAnnotations;

namespace NominaSystem.UI.Models;

public class LoginRequest
{
    public string UsuarioNombre { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
}
