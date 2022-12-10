using Microsoft.EntityFrameworkCore;

namespace Trade.Api;

public class TradeDbContext : DbContext
{
    //parameterless constructor creating a new DbContextOptionsBuilder and passing it to the base constructor
    public TradeDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Trade> Trades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trade>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Trade>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();
    }
}