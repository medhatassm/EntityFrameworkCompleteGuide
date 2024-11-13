using System.Data;
using System.Threading.Channels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;

#region Connection

var configuration = new ConfigurationBuilder()
    .AddJsonFile("AppSettings.json")
    .Build();

IDbConnection databaseConnection = new SqlConnection(configuration.GetSection("constr").Value);

#endregion

#region Select Statement Using Dapper

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

#endregion