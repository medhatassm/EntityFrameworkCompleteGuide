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

#region Multiple Query

var SQLQuery = "SELECT MIN(Balance) FROM Wallets;" + 
               "SELECT MAX(Balance) FROM Wallets;";

var multi = databaseConnection.QueryMultiple(SQLQuery);

Console.WriteLine($"Min: {multi.ReadSingle<decimal>()}\nMax: {multi.ReadSingle<decimal>()}");
// Another Way To Get Value From Multiple Query
// Console.WriteLine($"Min: {multi.Read<decimal>().Single()}\nMax: {multi.Read<decimal>().Single()}");

#endregion