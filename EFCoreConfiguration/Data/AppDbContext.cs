using EFCoreConfiguration.Data.Config;
using EFCoreConfiguration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCoreConfiguration.Data;

public class AppDbContext : DbContext
{
     // First Convention (Name Of Database have to match Name Of Db<Set> Property 
     public DbSet<User> Users { get; set; } = null!;
     public DbSet<Tweet> Tweets { get; set; } = null!;
     public DbSet<Comment> Comments { get; set; } = null!;

     
     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
          base.OnModelCreating(modelBuilder);
          
          // Override configuration using Fluent API
          // modelBuilder.Entity<User>().ToTable("tblUsers");
          // modelBuilder.Entity<Comment>().ToTable("tblComments");
          // modelBuilder.Entity<Tweet>().ToTable("tblTweets");
          //
          // modelBuilder.Entity<Comment>().Property(x => x.Id)
          //      .HasColumnName("CommentId");
          
          // Override configuration using Grouping Configuration calling By Individual
          // new UserConfiguration().Configure(modelBuilder.Entity<User>());
          // new CommentConfiguration().Configure(modelBuilder.Entity<Comment>());
          // new TweetConfiguration().Configure(modelBuilder.Entity<Tweet>());
          
          //Override configuration using Grouping Configuration Calling By Assembly
          modelBuilder.ApplyConfigurationsFromAssembly(
               typeof(UserConfiguration).Assembly);

     }

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     {
          base.OnConfiguring(optionsBuilder);

          var connection = new ConfigurationBuilder()
               .AddJsonFile("AppSettings.json")
               .Build().GetSection("constr").Value;

          optionsBuilder.UseSqlServer(connection);

     }
}