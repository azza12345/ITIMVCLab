using Microsoft.EntityFrameworkCore;

namespace MVCLab.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

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
