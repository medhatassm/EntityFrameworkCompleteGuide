using System.Data;
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

#region Insert Statement Using Dapper


var walletToInsert = new Wallet
{
    Holder = "Ayman",
    Balance = 7000m
};

var SQLQuery = "INSERT INTO Wallets (Holder, Balance)"
               + $" VALUES (@Holder , @Balance)"
               // Select ID
               + "SELECT CAST(SCOPE_IDENTITY() AS INT)";

// With Dapper Easy to Sent Parameter

databaseConnection.Execute(SQLQuery , new {Holder =walletToInsert.Holder ,
    Balance = walletToInsert.Balance});

#endregion

#region Insert Statement Using Dapper Return Id

var parameters = new
{
    Holder = walletToInsert.Holder,
    Balance = walletToInsert.Balance
};

walletToInsert.Id = databaseConnection.Query<int>(SQLQuery, parameters).Single();

Console.WriteLine(walletToInsert);

#endregion
