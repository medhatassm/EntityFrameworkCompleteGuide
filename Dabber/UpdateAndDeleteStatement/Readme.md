## Using Update And Delete Statement

### Update Statement

```csharp
var walletToUpdate = new Wallet
{
    Id = 2007,
    Holder = "Ammar",
    Balance = 3000m
};

var SQLUpdateQuery = "UPDATE Wallets SET Holder = @Holder , Balance = @Balance" +
               " WHERE Id = @Id";

databaseConnection.Execute(SQLUpdateQuery , new {
    Id = walletToUpdate.Id,
    Holder =walletToUpdate.Holder ,
    Balance = walletToUpdate.Balance});
```

### Delete Statement

```csharp
var walletToDelete = new Wallet
{
    Id = 2008,
};

var SQLDeleteQuery = "DELETE FROM Wallets WHERE Id = @Id";

databaseConnection.Execute(SQLDeleteQuery , new {
    Id = walletToDelete.Id, 
});
```