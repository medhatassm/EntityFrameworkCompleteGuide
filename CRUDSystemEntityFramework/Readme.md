## Happy Code With Entity Framework Core (CRUD System)

> After Creating two main class (Map class , Context class), go to Program.cs and do the following.
>

```csharp
using var context = new AppDbContext();
```

### Insert Data

```csharp
var newWallet = new Wallet()
{
    Holder = "Mazen",
    Balance = 1200m
};

context.Wallets.Add(newWallet); // Inserted On Memory
context.SaveChanges(); // Inserted On DB
```

let me explain something here, when compiler reach this line `context.Wallets.Add(newWallet);` it will save the change process of Wallet class into memory only but will not update data source (database).

so we have to write this line `context.SaveChanges();` to make sure the compiler update the database to.

---

### Retrieve Collection Data Or Single Item

```csharp
// Retrieve Collection Of Item
foreach (var wallet in context.Wallets)
{
    Console.WriteLine(wallet);
}

//===========================================================================

// Retrieve Singel Item

var item = context.Wallets.FirstOrDefault(x=> x.Id == 2);
Console.WriteLine(item);
```

As you see in Entity Framework we use LINQ not SQL Query and that make Entity Framework easier than rest of ORM System.

---

### Update Data

```csharp
var updateWallet = context.Wallets.Single(x => x.Id == 2003);

updateWallet.Balance += 1000; // Update On Memory

context.SaveChanges(); // Update On DB
```

---

### Delete Data

```csharp
var deleteWallet = context.Wallets.Single(x => x.Id == 3007);
context.Wallets.Remove(deleteWallet); // Delete On Memory
context.SaveChanges(); // Delete On DB
```

---

### Transaction

you know in transaction you have to make sure that all query you made have to success to make transaction process complete, so this how you can do it with entity framework

```csharp
using var transaction = context.Database.BeginTransaction();

// Transfer $500 from wallet id = 2002 to wallet id = 2003

var fromWallet = context.Wallets.Single(x => x.Id == 2002);
var toWallet = context.Wallets.Single(x => x.Id == 2003);

const decimal amount = 500m;

// Operation Number One (With Drew $500 From Wallet ID = 2002)
fromWallet.Balance -= amount;
context.SaveChanges();

// Operation Number Two (Deposit $500 From Wallet ID = 2003)
toWallet.Balance += amount;
context.SaveChanges();

transaction.Commit();
```

> **NOTE :** Every changed declared after transaction will not affected database until you write this line `transaction.Commit();` , and compiler will check it all process executed successfully, then it will make the effect to your database.
>