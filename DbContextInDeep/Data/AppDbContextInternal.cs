using DbContextInDeep.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DbContextInDeep.Data;

public class AppDbContextInternal : DbContext
{
    public DbSet<Wallet> Wallets { get; set; } = null!;
    
    // Internal Configuration
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        #region Connection String Configuration

        var connection = new ConfigurationBuilder()
            .AddJsonFile("AppSettings.json")
            .Build()
            .GetSection("constr").Value;

        optionsBuilder.UseSqlServer(connection);

        #endregion
    }
}