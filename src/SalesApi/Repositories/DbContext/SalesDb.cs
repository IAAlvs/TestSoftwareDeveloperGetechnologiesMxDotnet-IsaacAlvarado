using Microsoft.EntityFrameworkCore;
using SalesApi.Models;
namespace SalesApi.Repositories;

public class SalesDb : DbContext
{
    public SalesDb(DbContextOptions<SalesDb> options, IConfiguration config) : base(options) { 
        _config = config;
    }
    private readonly IConfiguration _config;
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_config["DbConnection"]!);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Person>()
            .HasIndex(person => person.Id)
            .IsUnique();
        builder.Entity<Person>()
            .HasIndex(person => person.Identification)
            .IsUnique();

        builder.Entity<Invoice>()
            .HasOne(p => p.Person)
            .WithMany(c => c.Invoices)
            .HasForeignKey(p => p.PersonId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}