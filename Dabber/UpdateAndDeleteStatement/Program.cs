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

#region Update Statemetn With Dapper

var walletToUpdate = new Wallet
{
    Id = 2007,
    Holder = "Ammar",
    Balance = 3000m
};

var SQLUpdateQuery = "UPDATE Wallets SET Holder = @Holder , Balance = @Balance" +
               " WHERE Id = @Id";


databaseConnection.Execute(SQLUpdateQuery , new {
    Id = walletToUpdate.Id,
    Holder =walletToUpdate.Holder ,
    Balance = walletToUpdate.Balance});

#endregion

#region Delete Statemetn With Dapper

var walletToDelete = new Wallet
{
    Id = 2008,
};

var SQLDeleteQuery = "DELETE FROM Wallets WHERE Id = @Id";


databaseConnection.Execute(SQLDeleteQuery , new {
    Id = walletToDelete.Id, 
});

#endregion