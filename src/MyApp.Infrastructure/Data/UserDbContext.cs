using Microsoft.EntityFrameworkCore;
using SF.BikeTheft.Domain.Entities;

namespace SF.BikeTheft.Infrastructure.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Roles)
            .HasConversion(
                u => string.Join(',', u),
                u => u.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
    }
}