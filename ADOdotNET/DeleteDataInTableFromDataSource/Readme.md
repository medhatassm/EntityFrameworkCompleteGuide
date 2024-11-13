## Delete Data From Table With ADO.NET

Using Execute Non-Query

```csharp

var command = new SqlCommand("DELETE FROM Wallets " + $"WHERE Id = @Id" , connection);

SqlParameter IdParameter = new SqlParameter
{
    ParameterName = "@Id",
    SqlDbType = SqlDbType.Int,
    Direction = ParameterDirection.Input,
    Value = 2006
};

command.Parameters.Add(IdParameter);

command.CommandType = CommandType.Text;

connection.Open();

if (command.ExecuteNonQuery() > 0)
{
    Console.WriteLine("Wallet Deleted Successfully");
}
else
{
    Console.WriteLine("Failed");
}
connection.Close();

```