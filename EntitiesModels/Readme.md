## The Entity Framework Core Model

conceptual model of an application’s domain

- Describes entity and relation ship between them.

### NuGet Package To Install

- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Configuration.json
- Microsoft.EntityFramework.(Data Provider (SQLServer, MySQL , Oracle))

### Entity Framework model have two main section

- Entities ⇒ Class that maps to database table (RDBMS)
- Context Object (DB Context) ⇒ Represent a session with the database querying and a saving data.

> This two section have to be exist for project that have entity framework core, to deal with your data base
>

### Entity

in simple term, a class that maps to database table (RDBMS) where each property maps to column.

```csharp
public class Wallet
{
    public int Id { get; set; }
    public string? Holder { get; set; }
    public decimal? Balance { get; set; }
    public override string ToString()
    {
        return $"[{Id}]- {Holder} - [Balance: ({Balance:C})]";
    }
}
```

---

### DB Context

the context object allows querying and saving data.

```csharp
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
```

- **DbSet<Class Name>**

  Represent collection of “Class Name” Entities, now this (table || class) in your Context Configuration you can Querying on it.

---

- **OnConfiguring**

  This is override method it call when you call your AppContextClass and make all entity framework configuration work on your project, and you set connection string inside it and sent it to DbContextOptionsBuilder Parameter, that make entity framework package know where to look for the data. (Connection String we talk about it inside Dapper and ADO.NET Section).