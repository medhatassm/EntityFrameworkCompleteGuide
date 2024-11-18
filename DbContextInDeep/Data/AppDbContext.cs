using DbContextInDeep.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbContextInDeep.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Wallet> Wallets { get; set; }
    
}