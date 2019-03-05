
[package-url]:   https://www.nuget.org/packages/ES.FX.Sql
[package-image]: https://img.shields.io/nuget/v/ES.FX.Sql.svg
[wiki-url]:      https://github.com/EmberStack/ES.FX.Sql/wiki

# ES.FX.Sql [![Build Status](https://dev.azure.com/emberstack/ES.FX-Public/_apis/build/status/ES.FX.Sql?branchName=master)](https://dev.azure.com/emberstack/ES.FX-Public/_build/latest?definitionId=6&branchName=master) [![package][package-image]][package-url]
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