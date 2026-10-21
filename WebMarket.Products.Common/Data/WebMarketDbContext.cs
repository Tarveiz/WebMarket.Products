using Microsoft.EntityFrameworkCore;
using WebMarket.Products.Common.Data.Configurations;
using WebMarket.Products.Common.Data.Entities.Products;
using WebMarket.Products.Common.Data.Entities.Users;

namespace WebMarket.Products.Common.Data;

/// <summary>
/// Контекст общей БД WebMarket.
/// </summary>
public class WebMarketDbContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public WebMarketDbContext(DbContextOptions<WebMarketDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }
}
