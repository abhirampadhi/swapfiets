using Microsoft.EntityFrameworkCore;

namespace SF.BikeTheft.Infrastructure.Data;

public class SwapfietsDbContext : DbContext
{
    public SwapfietsDbContext(DbContextOptions<SwapfietsDbContext> options)
        : base(options) { }

}
