// ToDo: 1- Package Manager Console (PMC)
//     Tools => Nuget Package Manager -> Package Manager Console
// ToDo: 2- Package Manager Console (PMC) Tools
//     Install-Package Microsoft.EntityFrameworkCore.Tools
// ToDo: 3- Install Nuget Page on Project Microsoft.EntityFrameworkCore.Desgin
//     Microsoft.EntityFrameworkCore.SqlServer
// ToDo: 4- Install Provider in the project Microsoft.EntityFramework.Sql
// ToDo: 5- Run Command (Full)
//     Scaffold-DbContext '[Connection String]' [Provider]

using ReverseEngineering;
using ReverseEngineering.Data;

using var context = new AppDbContext();

foreach (var item in context.Speakers)
{
    Console.WriteLine(item.FirstName + " " + item.LastName);
}