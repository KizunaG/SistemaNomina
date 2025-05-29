using NominaSystem.UI.Models;

public interface IEmpleadoService
{
    Task<List<EmpleadoDto>> ObtenerTodosAsync();
    Task SetAuthTokenAsync(string token);

    Task<bool> ValidarExpediente(int empleadoId);

}
