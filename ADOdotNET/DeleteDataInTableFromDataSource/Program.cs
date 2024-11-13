using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

#region Connection

var configuration = new ConfigurationBuilder()
    .AddJsonFile("AppSettings.json")
    .Build();

var connection = new SqlConnection(configuration.GetSection("constr").Value);

#endregion

#region Delete Element From DataSource Using Execute-NonQuery

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

#endregion