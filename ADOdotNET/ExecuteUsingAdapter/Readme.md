## Execute Query Using Adapter

using adapter in case you want to store your data into data table and using them later in your project without keep connection open. (Disconnected Mode On Memory)

```csharp
var SQL = "SELECT * FROM Wallets";

connection.Open();

SqlDataAdapter adapter = new SqlDataAdapter(SQL, connection);

DataTable dT = new DataTable();

adapter.Fill(dT); // Data Store Into This Data Table

connection.Close();

foreach (DataRow dR in dT.Rows)
{
    var wallet = new Wallet
    {
        Id = Convert.ToInt32(dR["Id"]),
        Holder = Convert.ToString(dR["Holder"]),
        Balance = Convert.ToDecimal(dR["Balance"]),
    };

    Console.WriteLine(wallet);
}

```