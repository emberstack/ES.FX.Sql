[package-url]:   https://www.nuget.org/packages/ES.FX.Sql
[package-image]: https://img.shields.io/nuget/v/ES.FX.Sql.svg
[build-url]:     https://sintari.visualstudio.com/ES.FX
[build-image]:   https://sintari.visualstudio.com/_apis/public/build/definitions/34e057ec-f09f-4d30-92f4-5895eeaa3f74/11/badge
[wiki-url]:      https://github.com/EmberStack/ES.FX.Sql/wiki

# ES.FX.Sql [![build][build-image]][build-url] [![package][package-image]][package-url]
SQL Server management classes and data adapter extensions for .NET


## Installation
Package Manager:
```shell
Install-Package ES.FX.Sql
```
.NET CLI:
```shell
dotnet add package ES.FX.Sql
```


## Usage
Managing SQL server and databases using SQL server management object
```csharp
var manager = new SqlServerManager(connectionString);
```

SQL Server management API
```csharp
//Query if server is SQL Azure
var isAzure = manager.Server.IsAzure();

//Async - Query if server is SQL Azure
isAzure = await manager.Server.IsAzureAsync();

//Query server properties by name
var property = manager.Server.GetProperty("propertyName");

//Async - Query server properties by name
property = await manager.Server.GetPropertyAsync("propertyName");
```


Database management API
```csharp
//Get the current database (Initial Catalog)
var database = manager.CurrentDatabase;

//Get the database by name
database = manager.Databases["databaseName"];

 //Create the database 
database.Create();

//Async - Create current database
await database.CreateAsync();

//Create the database with Azure tier details
database.Create(new AzureDatabaseTierDetails
{
    //Specify edition and service objective
    Edition = "standard",
    ServiceObjective = "S1",

    //If specified, elastic pool is used.
    ElasticPool = "PoolName"
});

//Async - Create the database with Azure tier details
await database.CreateAsync(new AzureDatabaseTierDetails
{
    //Specify edition and service objective
    Edition = "standard",
    ServiceObjective = "S1",

    //If specified, elastic pool is used.
    ElasticPool = "PoolName"
});


//Drop database
database.Drop(); 

//Async - Drop database
await database.DropAsync();


//Check if the database exists
var exists = database.Exists();

//Async - Check if the database exists
exists = await database.ExistsAsync();
```

## Documentation
Additional documentation may be found in the [Project Wiki][wiki-url]