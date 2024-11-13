## Execute Multiple Query

```csharp
var SQLQuery = "SELECT MIN(Balance) FROM Wallets;" + 
               "SELECT MAX(Balance) FROM Wallets;";

var multi = databaseConnection.QueryMultiple(SQLQuery);

Console.WriteLine($"Min: {multi.ReadSingle<decimal>()}\nMax: {multi.ReadSingle<decimal>()}");
// Another Way To Get Value From Multiple Query
// Console.WriteLine($"Min: {multi.Read<decimal>().Single()}\nMax: {multi.Read<decimal>().Single()}");
```