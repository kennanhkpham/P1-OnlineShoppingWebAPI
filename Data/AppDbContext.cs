// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using P1_OnlineShoppingWebAPI.Models;

namespace P1_OnlineShoppingWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
    }
}