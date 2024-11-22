using EntityTypesAndMapping.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Configuration;
using Snapshot = EntityTypesAndMapping.Entities.Snapshot;

namespace EntityTypesAndMapping.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    // When Remove DbSet Order Details, EF-Core still have it, but you can't directly
    // access it
    // public DbSet<OrderDetails> OrderDetails { get; set; }

    public DbSet<OrderWithDetailsViews> OrderWithDetailsViews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // We're Doing This Because we have schema to follow into database
        modelBuilder.Entity<Product>()
            .ToTable("Products", schema: "Inventory")
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<Order>()
            .ToTable("Orders", schema: "Sales")
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<OrderDetails>()
            .ToTable("OrderDetails", schema: "Sales")
            .HasKey(x => x.Id);
        
        // Ignore Entities (Exclude Entity)
        modelBuilder.Ignore<Snapshot>();
        
        // Set Default Schema To All Tables
        // modelBuilder.HasDefaultSchema("Sales");
        
        // Include Entities Without DbSet or include in another Entities
        modelBuilder.Entity<AuditEntry>().HasKey(x => x.Id);
        
        // Call Views From Data Source
        modelBuilder.Entity<OrderWithDetailsViews>()
            .ToView("OrderWithDetailsView")
            .HasNoKey();
        
        // Call Function Table-Value From Data Source
        modelBuilder.Entity<OrderBill>().HasNoKey()
            .ToFunction("GetOrderBill");
        
        base.OnModelCreating(modelBuilder);
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connection = new ConfigurationBuilder().AddJsonFile("appSettings.json")
            .Build().GetSection("connectionString").Value;
        optionsBuilder.UseSqlServer(connection);
        
        
    }
}