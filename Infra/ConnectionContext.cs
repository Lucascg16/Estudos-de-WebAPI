using Microsoft.EntityFrameworkCore;
using WebApi.Model;

namespace WebApi.Infra
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(
                "server=localhost;" +
                "port=5432;Database=employee_sample;" +
                "User Id=postgres;" +
                "Password=admin;");     
    }
}
