using System.Net.Http.Json;
using NominaSystem.UI.Models;
using NominaSystem.UI.Services.Interfaces;

namespace NominaSystem.UI.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;

    public AuthService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string?> LoginAsync(string correo, string contrasena)
    {
        var request = new LoginRequest
        {
            Correo = correo,
            Contrasena = contrasena
        };

        var response = await _http.PostAsJsonAsync("https://localhost:7122/api/Auth/login", request);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return result?.Token;
        }

        return null;
    }

    private class TokenResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}
