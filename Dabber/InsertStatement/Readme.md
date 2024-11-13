## Using Insert Statement

```csharp
var walletToInsert = new Wallet
{
    Holder = "Ayman",
    Balance = 7000m
};

var SQLQuery = "INSERT INTO Wallets (Holder, Balance)"
               + $" VALUES (@Holder , @Balance)"
               // Select ID to return it by using next code.
               + "SELECT CAST(SCOPE_IDENTITY() AS INT)";

// With Dapper Easy to Sent Parameter

databaseConnection.Execute(SQLQuery , new {Holder =walletToInsert.Holder ,
    Balance = walletToInsert.Balance});
```

### Also you can insert and return value

```csharp
var parameters = new
{
    Holder = walletToInsert.Holder,
    Balance = walletToInsert.Balance
};

walletToInsert.Id = databaseConnection.Query<int>(SQLQuery, parameters).Single();

Console.WriteLine(walletToInsert);
```