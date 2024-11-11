### ExecuteReader

```csharp

// Variable That Hold Query Statement And Connection As Command
var command = new SqlCommand("SELECT * FROM Wallets" , connection);
// You have to define the type of command.
command.CommandType = CommandType.Text;

// Before you execute any command you have to open connection
connection.Open();

while (command.ExecuteReader().Read())
{
    // Get Data and store it as wallet object
    var wallet = new Wallet
    {
        Id = command.ExecuteReader().GetInt32("Id"),
        Holder = command.ExecuteReader().GetString("Holder"),
        Balance = command.ExecuteReader().GetDecimal("Balance")
    };
    
    Console.WriteLine(wallet);
}

connection.Close()
```