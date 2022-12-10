using Kynetix.Common;
using Microsoft.EntityFrameworkCore;

namespace ReferenceData.Api;

public class ReferenceDataDbContext : DbContext
{
    public ReferenceDataDbContext(DbContextOptions<ReferenceDataDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Exchange> Exchanges { get; set; }
    public DbSet<Firm> Firms { get; set; }
    public DbSet<Instrument> Instruments { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Account>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Currency>()
        .HasKey(t => t.Id);

        modelBuilder.Entity<Currency>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Exchange>()
        .HasKey(t => t.Id);

        modelBuilder.Entity<Exchange>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Firm>()
        .HasKey(t => t.Id);

        modelBuilder.Entity<Firm>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Instrument>()
        .HasKey(t => t.Id);

        modelBuilder.Entity<Instrument>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();
    }
}