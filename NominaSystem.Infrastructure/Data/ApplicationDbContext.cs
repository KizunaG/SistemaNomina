using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NominaSystem.Domain.Entities;

namespace NominaSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ENTIDADES PRINCIPALES
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }

        // INFORMACIÓN ACADÉMICA Y DOCUMENTOS
        public DbSet<InformacionAcademica> InformacionAcademica { get; set; }
        public DbSet<DocumentoEmpleado> DocumentosEmpleado { get; set; }
        public DbSet<ConfiguracionExpediente> ConfiguracionExpediente { get; set; }
        public DbSet<ExpedienteEmpleado> ExpedientesEmpleado { get; set; }

        // NÓMINA Y AJUSTES
        public DbSet<Nomina> Nominas { get; set; }
        public DbSet<AjusteNomina> AjustesNomina { get; set; }

        // DESCUENTOS LEGALES
        public DbSet<DescuentoLegal> DescuentosLegales { get; set; }
        public DbSet<DetalleDescuentoNomina> DetallesDescuentoNomina { get; set; }

        // AUDITORÍA
        public DbSet<BitacoraAuditoria> BitacoraAuditoria { get; set; }

        // CONFIGURACIÓN OPCIONAL
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeo adicional si es necesario
            modelBuilder.Entity<Empleado>()
                .HasIndex(e => e.DPI)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.UsuarioNombre)
                .IsUnique();

            modelBuilder.Entity<ConfiguracionExpediente>()
                .HasIndex(c => c.TipoDocumento)
                .IsUnique();
        }
    }
}



