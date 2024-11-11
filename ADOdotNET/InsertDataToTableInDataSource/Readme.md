### Execute Non-Query

```csharp
// Read From user input
Console.WriteLine("Please Enter Your Name");
var holder = Console.ReadLine();
Console.WriteLine("Please Enter Your Balance");
decimal? balance = Convert.ToDecimal(Console.ReadLine());

// Store data as wallet object
var walletToInsert = new Wallet
{
    Holder = holder,
    Balance = balance
};

#region IF you want to add some spicific properties to parameters in data source

SqlParameter holderParameter = new SqlParameter
{
    ParameterName = "@Holder",
    SqlDbType = SqlDbType.VarChar,
    Direction = ParameterDirection.Input,
    Value = walletToInsert.Holder
};

SqlParameter balanceParameter = new SqlParameter
{
    ParameterName = "@Balance",
    SqlDbType = SqlDbType.Decimal,
    Direction = ParameterDirection.Input,
    Value = walletToInsert.Balance
};

#endregion

var command = new SqlCommand("INSERT INTO Wallets (Holder,Balance)" +
                             $"VALUES (@Holder , @Balance)"
												    , connection);

command.CommandType = CommandType.Text;

command.Parameters.Add(holderParameter);
command.Parameters.Add(balanceParameter);

connection.Open();

Console.WriteLine(command.ExecuteNonQuery() > 0
    ? $"wallet for {walletToInsert.Holder} added successfully"
    : $"wallet for {walletToInsert.Holder} was not added");

connection.Close();

```

### Execute Scaler

using this executed method when you want to return value after inserting process.

```csharp
 ...
 
 walletToInsert.Id = (int) command.ExecuteScalar();
 Console.WriteLine($"wallet for {walletToInsert} added successfuly");
 
 ..
```

---

### Stored Procedure

Insert Using Existing Procedure into your data source

```csharp
 ...
 
 var commandProcedure = new SqlCommand("AddWallet", connection);
 commandProcedure.CommandType = CommandType.StoredProcedure;
 
 ...
```