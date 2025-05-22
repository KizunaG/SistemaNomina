using Microsoft.EntityFrameworkCore;
using NominaSystem.Infrastructure.Data;
using NominaSystem.Application.Interfaces;
using NominaSystem.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexión a la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de los servicios
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<ICargoService, CargoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IInformacionAcademicaService, InformacionAcademicaService>();
builder.Services.AddScoped<IConfiguracionExpedienteService, ConfiguracionExpedienteService>();
builder.Services.AddScoped<IDocumentoEmpleadoService, DocumentoEmpleadoService>();







var app = builder.Build();

// Configuración del entorno
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

