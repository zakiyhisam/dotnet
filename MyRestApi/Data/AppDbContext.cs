using Microsoft.EntityFrameworkCore;
using MyRestApi.Models;

namespace MyRestApi.Data;

public class AppDbContext: DbContext
{
    // The constructor hands configuration options (like connection string) to EF Core
    public AppDbContext(DbContextOptions<AppDbContext> options)
    {
        
    }
    public DbSet<Product> Products {get; set;}
}