## Transaction

Execute Collection of query statements but all query statements have to success or all will be failed.

```csharp
var command = connection.CreateCommand();

command.CommandType = CommandType.Text;

connection.Open();

SqlTransaction transaction = connection.BeginTransaction();
command.Transaction = transaction;

// The 2 Query Have to Executed Successfully
try
{
    command.CommandText = "UPDATE Wallets SET Balance = Balance - 1000 WHERE Id = 2";
    command.ExecuteNonQuery();
    
    command.CommandText = "UPDATE Wallets SET Balance = Balance + 1000 WHERE Id = 3";
    command.ExecuteNonQuery();
    
    transaction.Commit();
    Console.WriteLine("Transaction Complete Successfully");
}
catch (Exception e)
{
    try
    {
        transaction.Rollback();
    }
    catch (Exception exception)
    {
        // Log Error
    }
    
}
finally
{
    try
    {
        connection.Close();
    }
    catch (Exception e)
    {
       // Log Error
    }
}

connection.Close();
```