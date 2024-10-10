using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Benefit> Benefits { get; set; }
    public DbSet<EmployeeBenefit> EmployeeBenefits { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeBenefit>()
            .HasIndex(eb => new { eb.EmployeeId, eb.BenefitId })
            .IsUnique();
    }
}