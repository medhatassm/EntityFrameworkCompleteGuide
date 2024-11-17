using EntitiesModels;

using var context = new AppDbContext();

#region Retrieve Collection Of Data

foreach (var wallet in context.Wallets)
{
    Console.WriteLine(wallet);
}

#endregion

#region Retrieve One Item

var item = context.Wallets.FirstOrDefault(x=> x.Id == 2);
Console.WriteLine(item);

#endregion

#region Insert Data Into Table

var newWallet = new Wallet()
{
    Holder = "Mazen",
    Balance = 1200m
};

context.Wallets.Add(newWallet); // Inserted On Memory
context.SaveChanges(); // Inserted On DB

#endregion

#region Update Data

// Update Wallet ID = 2003 Set Balance Increse By 1000
var updateWallet = context.Wallets.Single(x => x.Id == 2003);

updateWallet.Balance += 1000; // Update On Memory

context.SaveChanges(); // Update On DB

#endregion

#region Delete Data

var deleteWallet = context.Wallets.Single(x => x.Id == 3007);
context.Wallets.Remove(deleteWallet); // Delete On Memory
context.SaveChanges(); // Delete On DB

#endregion

#region Query Data

var walletHigherThat5000 = context.Wallets.Where(x => x.Balance > 5000);

 foreach (var wallet in walletHigherThat5000)
 {
     Console.WriteLine(wallet);
}

#endregion

#region Transaction

using var transaction = context.Database.BeginTransaction();

// Transfer $500 from wallet id = 2002 to wallet id = 2003

var fromWallet = context.Wallets.Single(x => x.Id == 2002);
var toWallet = context.Wallets.Single(x => x.Id == 2003);

var amount = 500m;

// Operation Number One (With Drew $500 From Wallet ID = 2002)
fromWallet.Balance -= amount;
context.SaveChanges();

// Operation Number Two (Deposit $500 From Wallet ID = 2003)
toWallet.Balance += amount;
context.SaveChanges();

transaction.Commit();

#endregion