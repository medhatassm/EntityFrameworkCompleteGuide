## Transaction

```csharp
// Transferred 2000
// To: 2005 Mena  8000
// From: 2003 Bora 3500

decimal amountToTransferred = 2000m;

using (var transactionScope = new TransactionScope())
{
    var walletFrom = databaseConnection.QuerySingle<Wallet>("SELECT * FROM Wallets WHERE Id = @Id",
        new { Id = 2003 });

    var walletTo = databaseConnection.QuerySingle<Wallet>("SELECT * FROM Wallets WHERE Id = @Id",
        new { Id = 2005 });

    databaseConnection.Execute("UPDATE Wallets SET Balance = @Balance" +
                               " WHERE Id = @Id",
        new
        {
            Id = walletFrom.Id,
            Balance = walletFrom.Balance - amountToTransferred
        });
    
    databaseConnection.Execute("UPDATE Wallets SET Balance = @Balance" +
                               " WHERE Id = @Id",
        new
        {
            Id = walletTo.Id,
            Balance = walletTo.Balance + amountToTransferred
        });
    
    transactionScope.Complete();
}
```