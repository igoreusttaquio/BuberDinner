using BuberDinner.Domain.MenuAggregate;
using Microsoft.EntityFrameworkCore;

namespace BuberDinner.Infrastructure.Persistence;

public class BuberDinnerDbContext(DbContextOptions<BuberDinnerDbContext> options) : DbContext(options)
{
    public DbSet<Menu> Menus { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BuberDinnerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
