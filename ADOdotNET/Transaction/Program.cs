using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

#region Connection

var configuration = new ConfigurationBuilder()
    .AddJsonFile("AppSettings.json")
    .Build();

var connection = new SqlConnection(configuration.GetSection("constr").Value);

#endregion

#region Execute Collection of query statements (Transaction)

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

#endregion