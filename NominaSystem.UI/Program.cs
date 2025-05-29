using NominaSystem.UI.Services.Interfaces;
using NominaSystem.UI.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Server;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Blazor y páginas
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// HttpClient apuntando al backend correcto (puerto 7122 del WebAPI)
builder.Services.AddScoped(sp =>
{
    var client = new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7122/") //backend API
    };
    return client;
});

// Servicios inyectables
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<NominaService>();

// Componentes interactivos
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
// Mostrar errores de circuito
builder.Services.Configure<CircuitOptions>(options =>
{
    options.DetailedErrors = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
