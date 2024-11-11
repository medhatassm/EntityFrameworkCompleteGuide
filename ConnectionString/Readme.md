## Connection String

is **a semicolon-delimited list of key/value parameter pairs**: `keyword1=value; keyword2=value etc…;`

```json
{
  "constr": "Server=localhost; Database=DigitalCurrency; User Id=SA; Password=CisL2249; TrustServerCertificate=True;"
}
```
This Example for connection to Docker image (MS SQL Server) With Azure Studio, MS SQL Management will have different connection string (search for your data source).

**In Main Class (Program.cs)**

```csharp
// Get Json File That Have Connection String
var configuration = new ConfigurationBuilder()
    .AddJsonFile("AppSettings.json")
    .Build();

// Make Connection Variable To Connect to SQL
var connection = new SqlConnection(configuration.GetSection("constr").Value);
```
Now We have connection as variable and can use it in our project.