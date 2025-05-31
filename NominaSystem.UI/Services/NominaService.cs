using NominaSystem.UI.Models;
using System.Net.Http.Json;

public class NominaService
{
    private readonly HttpClient _httpClient;

    public NominaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<NominaDto>> GetNominasAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<NominaDto>>("api/Nominas");
    }

    public async Task DeleteNominaAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/Nominas/{id}");
    }
}


