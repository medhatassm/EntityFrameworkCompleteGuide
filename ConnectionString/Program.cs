using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

#region ConnectionString

// Get Json File That Have Connection String
var configuration = new ConfigurationBuilder()
    .AddJsonFile("AppSettings.json")
    .Build();

// Get Value Of "constr" From JSON File
Console.WriteLine(configuration.GetSection("constr").Value);

// Make Connection Variable To Connect to SQL
var connection = new SqlConnection(configuration.GetSection("constr").Value);

#endregion

