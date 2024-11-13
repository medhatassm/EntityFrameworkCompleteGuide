## Using Select Statement

- Dynamic Query: Return data table without define its type.
- Typed Query: Return data table with define its type as class.

```csharp
var SQLQuery = "SELECT * FROM Wallets";

Console.WriteLine("-------- using Dynamic query ---------");

var resultAsDynamic = databaseConnection.Query(SQLQuery);

foreach (var item in resultAsDynamic)
{
    Console.WriteLine(item);
}

Console.WriteLine("-------- using Typed query ---------");

var resultAsTyped = databaseConnection.Query<Wallet>(SQLQuery);

foreach (var wallet in resultAsTyped)
{
    Console.WriteLine(wallet);
}
```