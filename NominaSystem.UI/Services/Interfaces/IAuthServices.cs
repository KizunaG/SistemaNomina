namespace NominaSystem.UI.Services.Interfaces;

public interface IAuthService
{
    Task<string?> LoginAsync(string correo, string contrasena);
}
