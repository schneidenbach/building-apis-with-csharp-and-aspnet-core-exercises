using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;

public class AppDbContext : DbContext
{
    private readonly ISystemClock _systemClock;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AppDbContext(
        DbContextOptions<AppDbContext> options, 
        ISystemClock systemClock, 
        IHttpContextAccessor httpContextAccessor
    ) : base(options)
    {
        this._systemClock = systemClock;
        this._httpContextAccessor = httpContextAccessor;
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

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                entry.Entity.CreatedOn = _systemClock.UtcNow.UtcDateTime;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedBy = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                entry.Entity.LastModifiedOn = _systemClock.UtcNow.UtcDateTime;
            }
        }
    }
}