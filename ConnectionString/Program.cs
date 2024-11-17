using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

#region ConnectionString

// Get Json File That Have Connection String
var configuration = new ConfigurationBuilder()
    .AddJsonFile("AppSettings.json")
    .Build();

// Make Connection Variable To Connect to SQL
var connection = configuration.GetSection("constr").Value;

// Get Value Of "constr" From JSON File
Console.WriteLine(connection);


#endregion

