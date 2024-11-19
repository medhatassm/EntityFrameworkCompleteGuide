# Configuration

## Overview

### How you can use EF-Core to Deal With data table?

you have three option to follow:

1. **Database First ⇒ DB → Model** (generate model from existing DB) Not Recommended.
2. **Model First ⇒ DB ← Model** (hand code model to match DB).
3. **Code First ⇒ DB ← Model** (create a database from the model) Recommended.

This Called “ Convention Over Configuration Paradigm “.

### How you can set configuration of database to your project?

now there is two option one is default and other to manually set specific configuration.

1. Configuration By Convention (Default One)
2. Override Configuration (Manually)

---

## Configuration By Convention

**it has its own rule to follow.**

- First Convention ⇒ **Name of Data Table should match of DbSet<Class Name>**

```csharp
public DbSet<User> Users { get; set; }
public DbSet<Tweet> Tweets { get; set; }
public DbSet<Comment> Comments { get; set; }
```

![Screenshot 2024-11-19 at 8.44.02 PM.png](https://prod-files-secure.s3.us-west-2.amazonaws.com/ac8d2e56-c643-48fc-af41-f14c17164785/05584894-2371-48e3-b0e2-3303dce8f861/Screenshot_2024-11-19_at_8.44.02_PM.png)

- Second Convention ⇒ its called **Primary Convention**, primary key name should match this:`[Id , id , ID] or [{ClassName}Id]` in both class and table.
- Third Convention ⇒ **Column property mismatch**, property name in class should match column name in data source.

```csharp
public int UserId { get; set; }
public string? Username { get; set; }
```

![Screenshot 2024-11-19 at 8.50.01 PM.png](https://prod-files-secure.s3.us-west-2.amazonaws.com/ac8d2e56-c643-48fc-af41-f14c17164785/d0be3df1-ec27-49c3-9985-1f3b9432ddf1/Screenshot_2024-11-19_at_8.50.01_PM.png)

---

## Override Configuration

### Data Annotation

you can use data annotation to override configuration but it have some issue with clean code, because you will override each configuration in each class (entity) in your project

- lot of code.
- not match clean code.

```csharp
[Table("tblUsers")]
public class User
{
    public int UserId { get; set; }
    public string? Username { get; set; }
}
```

and there is more **Data Annotation Attribute** we will use it when it needed but for now, its good to know how you declare it and what it used for 😊

---

### Fluent API

this better than Data Annotation, this allow you to set all your configuration on one place using `OnModelCreating` override function, so it help you to organize your code and set configuration without get your class (entity) dirty.

```csharp
     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
          base.OnModelCreating(modelBuilder);
          
          // Override configuration using Fluent API
          modelBuilder.Entity<User>().ToTable("tblUsers");
          modelBuilder.Entity<Comment>().ToTable("tblComments");
          modelBuilder.Entity<Tweet>().ToTable("tblTweets");
      }
```

---

### Grouping Configuration

its the same about Fluent API, but this allow you to set all configuration of each entity in one place (Class) and call that class `onModelCreating` override function like this:

UserConfiguration.cs

```csharp
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("tblUsers");
        
    }
}
```

- now we have class called UserConfiguration that inherit from IEntityTypeConfiguration Interface, this class allow you to set all configuration you need for **User** entity in one place.
- then let’s call this configuration
    - you can call configuration from class using two option:
        1. Individual

           and this option have one issue, that you have call each configuration class one by one.

            ```csharp
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                      base.OnModelCreating(modelBuilder);
                      
                      // Override configuration using Grouping Configuration calling By Individual
                      new UserConfiguration().Configure(modelBuilder.Entity<User>());
                      new CommentConfiguration().Configure(modelBuilder.Entity<Comment>());
                      new TweetConfiguration().Configure(modelBuilder.Entity<Tweet>());
                      
            }
            ```

        2. Assembly

           This one is perfect, just one line call all configuration class.

            ```csharp
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                      base.OnModelCreating(modelBuilder);
            					modelBuilder.ApplyConfigurationsFromAssembly(
                           typeof(UserConfiguration).Assembly);
             }
            ```