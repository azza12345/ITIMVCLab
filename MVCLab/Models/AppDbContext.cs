using Microsoft.EntityFrameworkCore;

namespace MVCLab.Models
{
    public class AppDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   => optionsBuilder.UseSqlServer("Server= .; Database=MVCLab; Integrated Security=True; Encrypt=True;TrustServerCertificate=True;");

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Employee>()
                .HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.CompanyId);

        }
    }
}
