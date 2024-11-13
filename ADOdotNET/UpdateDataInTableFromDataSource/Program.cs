using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

#region Connection

var configuration = new ConfigurationBuilder()
    .AddJsonFile("AppSettings.json")
    .Build();

var connection = new SqlConnection(configuration.GetSection("constr").Value);

#endregion

#region Update Element Into DataSource Using Execute-NonQuery

var command = new SqlCommand("UPDATE Wallets SET Holder = @Holder , Balance = @Balance " +
                             "WHERE Id = @Id" , connection);

SqlParameter IdParameter = new SqlParameter
{
    ParameterName = "@Id",
    SqlDbType = SqlDbType.Int,
    Direction = ParameterDirection.Input,
    Value = 1
};

SqlParameter holderParameter = new SqlParameter
{
    ParameterName = "@Holder",
    SqlDbType = SqlDbType.VarChar,
    Direction = ParameterDirection.Input,
    Value = "Ahmed"
};

SqlParameter balanceParameter = new SqlParameter
{
    ParameterName = "@Balance",
    SqlDbType = SqlDbType.Decimal,
    Direction = ParameterDirection.Input,
    Value = "9000"
};

command.Parameters.Add(IdParameter);
command.Parameters.Add(holderParameter);
command.Parameters.Add(balanceParameter);

command.CommandType = CommandType.Text;

connection.Open();

if (command.ExecuteNonQuery() > 0)
{
    Console.WriteLine("Wallet Update Successfully");
}
else
{
    Console.WriteLine("Faild");
}
connection.Close();

#endregion
