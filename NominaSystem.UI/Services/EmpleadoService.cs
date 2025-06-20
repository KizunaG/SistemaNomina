﻿using System.Net.Http.Headers;
using System.Net.Http.Json;
using NominaSystem.UI.Models;
using NominaSystem.UI.Services.Interfaces;

public class EmpleadoService : IEmpleadoService
{
    private readonly HttpClient _http;

    public EmpleadoService(HttpClient http)
    {
        _http = http;
    }

    public async Task SetAuthTokenAsync(string token)
    {
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<List<EmpleadoDto>> ObtenerTodosAsync()
    {
        var empleados = await _http.GetFromJsonAsync<List<EmpleadoDto>>("api/Empleado");
        return empleados ?? new List<EmpleadoDto>();
    }
    public async Task<bool> ValidarExpediente(int empleadoId)
    {
        var response = await _http.GetAsync($"api/ExpedienteEmpleado/validar/{empleadoId}");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return bool.Parse(json);
        }
        return false;
    }

}
