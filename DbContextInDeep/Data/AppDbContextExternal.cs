using DbContextInDeep.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbContextInDeep.Data;

public class AppDbContextExternal : DbContext
{
    public DbSet<Wallet> Wallets { get; set; }

    // External Configuration
    public AppDbContextExternal(DbContextOptions options) : base(options)
    {
        
    }
}