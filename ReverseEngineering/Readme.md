# EF-Core Model & DB Schema in Sync

## Introduction

**how to make your database schema match the code Or vice versa?**

we have two options to do it:

1. **Code source of truth (Migrations).**
2. **DB Schema source of truth (Reverse Engineering).**

---

# Reverse Engineering

This mean you use to EF-Core with (Database First) Scaffold.

- process of scaffolding entity type classes and DbContext based on schema

- **How It Work?**
    - Reefing Db Schema
    - Generate EF-Core Model
    - Uses EF-Core Model To Generate Code

- **Limitations:**
    - Providers Not Equal
    - DataType Support / Provider
    - Concurrency Token in EF-Core Issue
- **Pre-Requisite**
    - PMC Tool / .Net CLI Tool
    - Microsoft.EntityFrameworkCore.Desgin (NuGet package).
    - Install provider for database schema (NuGet Package) based on your provider.
- **Is Popular When**
    - Db Designed by DBA.
    - Entities to be Created by EF.
    - Manual changed possible.

## Reverse Engineering Scaffolding using (PMC || .Net CLI)

both is the same of what they will make, but there are some different while typing the command.

### PMC

- Using if you work on Windows OS and have Visual Studio Community.
- Work Only at Visual Studio Community.

### .NET CLI

- Included in .NET SDK
- can use in Windows OS / Mac OS / Linux OS

  > you just need to install EF tools by using this command in your terminal or CMD
  >

    ```bash
    dotnet tool install --global dotnet-ef
    ```


while I am using macOS and JetBrains Rider, so I will go to use .NET CLI in next section, you can use ChatGPT to convert .NET CLI command to PMC command, and you will it's the same but have small different 😂.

---

## Install NuGet Package Using .NET CLI

After make your terminal have project path not solution , using this command to install package.

```bash
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

after that install provider (I use SQL Server) so I use this command, ask ChatGPT if you use another provider like (MySQL , Azure, …)

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

---

## Using .NET CLI to get Database Schema As Coding

```bash
dotnet ef dbcontext scaffold "<ConnectionString>" <Provider> [Options]
```

this command will convert your database schema to EF-Core Model (DbContext , Entities).

- Connection String: like default, as one you set in AppSettings.json.
- Provider: your NuGet package Provider Mine is SQL Server so i set it to `Microsoft.EntityFrameworkCore.SqlServer`
- Options: Some Configuration you can set it manually, so you can control how you want the EF-Core Modeling your DB like:
    - Name of DbContext Class by using  `--context AppDbContext`
    - Using Data Annotations not Fluent API by using  `--data-annotations`
    - Make Compiler put Entities class into folder and DbContext to another folder by using  `--context-dir Data --output-dir Entities`

Final Code Will Be Like this if you use SQLServer with Azure Image into Mac OS

```bash
dotnet ef dbcontext scaffold "Server=localhost; Database=TechTalk; User Id=SA; Password=CisL2249; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --context AppDbContext --context-dir Data --output-dir Entities --schema dbo
```

---