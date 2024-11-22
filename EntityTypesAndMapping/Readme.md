# Entity Types & Mapping

## Configuring Table and Schema

if there a schema definition into your database then you have to define it into EF-Core configuration to make sure your project don’t get logical error

you define schema like this:

- Using Fluent API Configuration

AppDbContext.cs

```csharp
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .ToTable("Products", schema: "Inventory")
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<Order>()
            .ToTable("Orders", schema: "Sales")
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<OrderDetails>()
            .ToTable("OrderDetails", schema: "Sales")
            .HasKey(x => x.Id);
            
        base.OnModelCreating(modelBuilder);
    }
```

- Using Data Annotation

{ClassName}.cs

```csharp
[Table("Orders" , Schema = "Sales")]
public class Order
{
    [Key]
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string? CustomerEmail { get; set; }
    public List<OrderDetails> OrderDetailsList { get; set; } = new List<OrderDetails>();
}
```

---

## Exclude Entity

when you have class that not belong to your database and you want to add it to EF-Core and use it with DbContext, you can make it using the code below

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
			modelBuilder.Ignore<Snapshot>();
			
			//....
}
```

Program.cs

```csharp
using var context = new AppDbContext();

Console.WriteLine($"{item.Name} \t\n........ loaded at" +
                      $"{item.Snapshot.LoadedAt.ToString("yy-MM-dd hh:mm")}" +
                      $"\nVersion: {item.Snapshot.Version}");
```

---

## Including Entity

now what if we want to use some entities without using DbSet properties

```csharp
// Include Entities Without DbSet or include in another Entities
        modelBuilder.Entity<AuditEntry>().HasKey(x => x.Id);
```

- This AuditEntry Class now available to Context

```csharp
var audit = new AuditEntry() {UserName = "Medhat" , Action = "Read Order Count"};
context.Set<AuditEntry>().Add(audit); // Not Save it into data source (This is not EF Entities)
```

---

## Mapping View & Table Value Function

when you create view or Functions with SQL and save it, let’s include this into our project using EF-Core.

AppDbContext.cs

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		    //..
		    
        // Call Views From Data Source
        modelBuilder.Entity<OrderWithDetailsViews>()
            .ToView("OrderWithDetailsView")
            .HasNoKey();
            
        // Call Function Table-Value From Data Source
        modelBuilder.Entity<OrderBill>().HasNoKey()
            .ToFunction("GetOrderBill");
        
        //..
    }
```

Program.cs

```csharp
// Calling Views From Data Source
foreach (var item in context.OrderWithDetailsViews)
{
    Console.WriteLine(item);
}

// Calling Function Table-Value From Data Source
var orderBillDetails = context.Set<OrderBill>()
    .FromSqlInterpolated($"SELECT * FROM GetOrderBill({1})")
    .ToList();

foreach (var item in orderBillDetails)
{
    Console.WriteLine(item);
}
```

---