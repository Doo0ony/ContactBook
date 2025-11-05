using ContactBook.Domain.Entities;
using ContactBook.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Infrastructure.Data;

internal class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Phone> Phones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}