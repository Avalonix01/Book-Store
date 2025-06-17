using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Persistence;

public class StoreDbContext(DbContextOptions<StoreDbContext> opts) : DbContext(opts)
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
    }
}