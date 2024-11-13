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

#region Select Statement Using Execute Reader

var SQL = "SELECT * FROM Wallets";

connection.Open();

SqlDataAdapter adapter = new SqlDataAdapter(SQL, connection);

DataTable dT = new DataTable();

adapter.Fill(dT); // Data Store Into This Data Table

connection.Close();

foreach (DataRow dR in dT.Rows)
{
    var wallet = new Wallet
    {
        Id = Convert.ToInt32(dR["Id"]),
        Holder = Convert.ToString(dR["Holder"]),
        Balance = Convert.ToDecimal(dR["Balance"]),
    };

    Console.WriteLine(wallet);
}

#endregion