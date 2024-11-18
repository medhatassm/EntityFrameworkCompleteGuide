# Deep Into DbContext

## Database Context

DbContext have two main section that every project should have

- UnitOfWork
- Repository Pattern

### UnitOfWork

pattern make you can integrate any change in query, and **UnitOfWork** know what changed have been done and make it happen into your data source (database).

### Repository Pattern

class or component make encapsulate to log in which is needed to make change into data source (database).

> in father section we will talk about this two in more deep, for now let’s talk about create AppDbContext class step by step.
>

---

## Create AppDbContext Class Step by step

1. inherit DbContext class.

    ```csharp
    public class AppDbContext: DbContext
    {
    }
    ```

2. For each entity (mapping class) in your project, create `DbSet<ClassName>` for it

    ```csharp
    public class AppDbContext: DbContext
    {
    	public DbSet<Wallet> Wallets { get; set; }
    }
    ```


## 3. Passing Configuration To DbContext

There are four way to pass configuration to your AppDbContext class

## Internal Configuration

DbContext is defined and configured directly within the project's code, usually in the DbContext file itself.

- It does not rely on DI or any type of separation.

**Features:**

1. Simple and easy to understand.
2. Suitable for small or quick projects.

**Disadvantages:**

1. It is difficult to manage and change the settings.
2. It makes the code less maintainable.

**Coding:**

AppDbContext.cs

```csharp
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
```

Program.cs

```csharp
using var internalContext = new AppDbContextInternal();

// This Line Won't Work Without Some Configuration in AppDbContext.
foreach (var item in internalContext.Wallets)
{
    Console.WriteLine(item);
}
```

---

### External Configuration

Defining the settings outside the core code of the DbContext, often using appsettings.json or any external configuration file.

- Then the settings are passed when creating the DbContext.

**Features:**

1. The ability to easily modify settings without the need to change the code.
2. More flexible, especially when dealing with multiple operating environments. (Development, Production).

**Defects:**

1. It requires extra effort to organize.

**Coding:**

AppDbContext.cs

```csharp
public class AppDbContextExternal : DbContext
{
    public DbSet<Wallet> Wallets { get; set; }

    // External Configuration
    public AppDbContextExternal(DbContextOptions options) : base(options)
    {
        
    }
}
```

Program.cs

```csharp
var connection = new ConfigurationBuilder()
    .AddJsonFile("AppSettings.json")
    .Build()
    .GetSection("constr").Value;

var optionBuilder = new DbContextOptionsBuilder();
optionBuilder.UseSqlServer(connection);

using var externalContext = new AppDbContextExternal(optionBuilder.Options);

foreach (var item in externalContext.Wallets)
{
    Console.WriteLine(item);
}
```

---

### Dependency Injection

Dependency Injection is used to pass DbContext to classes and services.

- It allows injecting DbContext only in the places where it is needed.

**Advantages:**

1. It makes testing the code easier using a mock DbContext.
2. It improves code organization and reduces dependencies.

**Disadvantages:**

1. Needs a good understanding of DI.

**Coding:**

AppDbContext.cs

```csharp
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Wallet> Wallets { get; set; }
    
}
```

Program.cs

```csharp
var con = new ConfigurationBuilder()
    .AddJsonFile("AppSettings.json")
    .Build()
    .GetSection("constr").Value;

var services = new ServiceCollection();

services.AddDbContext<AppDbContext>(options => options.UseSqlServer(con));

IServiceProvider serviceProvider = services.BuildServiceProvider();

using var dIContext = serviceProvider.GetService<AppDbContext>();

foreach (var item in dIContext!.Wallets)
{
    // Console.WriteLine(item);
}
```

---

### Context Factory

Instead of manually creating DbContext or relying entirely on DI, you use a Factory that creates DbContext on demand.

- It makes the code more organized and easier to maintain, especially in projects that require DbContext in different places.

**Advantages:**

1. Flexibility:

   It allows you to work with DbContext without relying on DI in projects that do not support it.

2. Support for different development environments:

   It facilitates setting up DbContext with different Connection Strings based on the environment.

3. Tests:

   Makes writing tests easier, as you can inject different settings.


**Coding:**

AppDbContext.cs

```csharp
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Wallet> Wallets { get; set; }
    
}
```

Program.cs

```csharp
var constr = new ConfigurationBuilder()
    .AddJsonFile("AppSettings.json")
    .Build()
    .GetSection("constr").Value;

var servicesFactory = new ServiceCollection();

servicesFactory.AddDbContextFactory<AppDbContext>(option =>
    option.UseSqlServer(constr));

IServiceProvider serviceProviderFactory = servicesFactory.BuildServiceProvider();

var contextFactory = serviceProviderFactory.GetService<IDbContextFactory<AppDbContext>>();

using var factory = contextFactory!.CreateDbContext();

foreach (var item in factory.Wallets)
{
    Console.WriteLine(item);
}
```

---

### Summary Comparing

| **Feature/Method** | **Internal Configuration** | **External Configuration** | **Dependency Injection** | **DbContext Factory** |
| --- | --- | --- | --- | --- |
| **Core Idea** | Configures DbContext directly within the class. | Configures settings in an external file like `appsettings.json`. | Uses DI to inject DbContext into classes. | Dynamically creates DbContext using a Factory. |
| **Ease of Use** | Simple and straightforward, especially for small projects. | Flexible and easily changeable between environments. | Well-organized, suitable for large projects. | Highly flexible for apps without DI support. |
| **Suitable Applications** | Small projects or prototypes. | Medium to large projects with multiple environments. | Large projects with DI dependencies. | CLI, WPF, WinForms applications or testing. |
| **Connection Strings Management** | Hardcoded in code (not flexible). | Stored externally, decoupled from code. | Retrieved directly from DI. | Dynamically configured in the Factory. |
| **Reusability** | Hard to reuse due to reliance on internal code. | Easy to reuse through external files. | Excellent due to DI organization. | Great for reuse across multiple environments. |
| **Support for Migrations** | Doesn't easily adapt to changing environments. | Works well with appsettings configurations. | Fully supported and straightforward. | Fully supported with EF Migrations. |
| **Unit Testing** | Very difficult. | Requires additional setup. | Excellent (easily supports mocking). | Ideal for creating contexts during testing. |
| **Complexity** | Very simple but lacks structure. | Moderate. | Relatively complex, requires DI understanding. | Complex without prior experience with factories. |

---

### Selection based on the condition

**Internal Configuration**:

Use it only for small projects or quick experiments.

**External Configuration**:

A good option if you need to separate settings from code, especially for medium-sized projects.

**Dependency Injection**:

Best for large projects or those requiring high organization and testing support.

**DbContext Factory**:

Suitable for WPF, WinForms, CLI applications, or any project that does not rely on DI, and it is also very powerful when dealing with multiple environments or operations like Migrations.

---

## Other Topic For DbContext

### Concurrency

Concurrency in the context of databases means managing modifications that occur on the same data at the same time by multiple users or processes.

- The goal is to prevent conflicts between modifications.

**Types of Concurrency:**

1. **Optimistic Concurrency:**
    - It is assumed that collisions are rare.
    - Collisions are checked only when attempting to save.
    - Suitable for applications where the number of users is small.
2. **Pessimistic Concurrency:**
    - It prevents collisions by locking the data during modification.
    - It is used when a collision is expected (such as banking applications).

**Coding**

```csharp
var tasks = new []
{
	Task.Factory.StartNew(()=> Method1())
	Task.Factory.StartNew(()=> Method2())
}
Task.WhenAll(tasks).ContinueWith(x=> {Console.WriteLine("Process Complete")});
```

Make sure you use **Async and Await** without it syntax error will throw, because of using same instance in deferent process.