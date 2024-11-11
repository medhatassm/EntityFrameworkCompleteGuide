using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;

#region Connection

var configuration = new ConfigurationBuilder()
    .AddJsonFile("AppSettings.json")
    .Build();

var connection = new SqlConnection(configuration.GetSection("constr").Value);

#endregion

#region Insert Data Using Execute Non-Query

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
     // For Execute Scalar
    //+ "SELECT CAST(scope_identity() AS INT)"
    , connection);

#region Insert Using Existing Procedure

// var commandProcedure = new SqlCommand("AddWallet", connection);
// commandProcedure.CommandType = CommandType.StoredProcedure;

#endregion

command.CommandType = CommandType.Text;

command.Parameters.Add(holderParameter);
command.Parameters.Add(balanceParameter);

connection.Open();

Console.WriteLine(command.ExecuteNonQuery() > 0
    ? $"wallet for {walletToInsert.Holder} added successfully"
    : $"wallet for {walletToInsert.Holder} was not added");

#region Return Data While Execute Command By Using Execute Scalary

// walletToInsert.Id = (int) command.ExecuteScalar();
// Console.WriteLine($"wallet for {walletToInsert} added successfuly");

#endregion


connection.Close();

#endregion