using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NominaSystem.Infrastructure.Data;

namespace NominaSystem.Infrastructure.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            //cadena de conexión local
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=SistemaNominaEF;Trusted_Connection=True;TrustServerCertificate=True;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
