using Microsoft.EntityFrameworkCore;
using NominaSystem.Infrastructure.Data;
using NominaSystem.Application.Interfaces;
using NominaSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text; // Para Encoding
using Microsoft.IdentityModel.Tokens; // Para SymmetricSecurityKey y TokenValidationParameters



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
builder.Services.AddScoped<IExpedienteEmpleadoService, ExpedienteEmpleadoService>();
builder.Services.AddScoped<INominaService, NominaService>();
builder.Services.AddScoped<IAjusteNominaService, AjusteNominaService>();
builder.Services.AddScoped<IDescuentoLegalService, DescuentoLegalService>();
builder.Services.AddScoped<IDetalleDescuentoNominaService, DetalleDescuentoNominaService>();

//Login
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

var app = builder.Build();

// Configuración del entorno
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

