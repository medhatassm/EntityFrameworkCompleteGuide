using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntitiesModels;

public class AppDbContext : DbContext
{
    // Represent Collection Of All Entities
    public DbSet<Wallet> Wallets { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        
        // Connection String
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("AppSettings.json")
            .Build();

        // Make Connection Variable To Connect to SQL
        var connection = configuration.GetSection("constr").Value;
        
        // Pass Connection Value To DB Context Configuration
        optionsBuilder.UseSqlServer(connection);

    }
}