using NominaSystem.UI.Models;

namespace NominaSystem.UI.Services.Interfaces;

public interface IAuthService
{
    Task<string?> LoginAsync(string correo, string contrasena);
    Task<bool> RegistrarAsync(RegisterRequest request);
}
