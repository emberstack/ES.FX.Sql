using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ES.FX.Sql.Server.Azure;

namespace ES.FX.Sql.Server
{
    internal class SqlDatabase : ISqlDatabase
    {
        private readonly SqlConnectionStringBuilder _builder;
        private readonly SqlServer _server;

        internal SqlDatabase(SqlConnectionStringBuilder builder)
        {
            _builder = builder;
            _server = new SqlServer(_builder);
            Name = _builder.InitialCatalog;
        }

        public string Name { get; }

        public void Create(AzureDatabaseTierDetails azureDetails = null)
        {
            using (var connection = SqlConnectionFactory.CreateAndOpen(_builder.ToMasterString()))
            using (var command = new SqlCommand(Commands.Database_Create, connection))
            {
                if (azureDetails != null && _server.IsAzure())
                    if (!string.IsNullOrWhiteSpace(azureDetails.ElasticPool))
                        command.CommandText = Commands.Database_Create_Azure_Pool
                            .Replace("@ElasticPool", azureDetails.ElasticPool);
                    else
                        command.CommandText = Commands.Database_Create_Azure_Single
                            .Replace("@Edition", azureDetails.Edition)
                            .Replace("@ServiceObjective", azureDetails.ServiceObjective);
                command.CommandText = command.CommandText.Replace("@Database", _builder.InitialCatalog);

                command.ExecuteNonQuery();
            }
        }

        public async Task CreateAsync(AzureDatabaseTierDetails azureDetails = null)
        {
            using (var connection = await SqlConnectionFactory.CreateAndOpenAsync(_builder.ToMasterString()))
            using (var command = new SqlCommand(Commands.Database_Create, connection))
            {
                if (azureDetails != null && await _server.IsAzureAsync())
                    if (!string.IsNullOrWhiteSpace(azureDetails.ElasticPool))
                        command.CommandText = Commands.Database_Create_Azure_Pool
                            .Replace("@ElasticPool", azureDetails.ElasticPool);
                    else
                        command.CommandText = Commands.Database_Create_Azure_Single
                            .Replace("@Edition", azureDetails.Edition)
                            .Replace("@ServiceObjective", azureDetails.ServiceObjective);
                command.CommandText = command.CommandText.Replace("@Database", _builder.InitialCatalog);

                await command.ExecuteNonQueryAsync();
            }
        }


        public void Drop(bool closeConnections = false)
        {
            using (var connection = SqlConnectionFactory.CreateAndOpen(_builder.ToMasterString()))
            using (var command =
                new SqlCommand(closeConnections ? Commands.Database_Drop_CloseConnections : Commands.Database_Drop,
                    connection))
            {
                command.CommandText = command.CommandText.Replace("@Database", _builder.InitialCatalog);
                command.ExecuteNonQuery();
            }
        }

        public async Task DropAsync(bool closeConnections = false)
        {
            using (var connection = await SqlConnectionFactory.CreateAndOpenAsync(_builder.ToMasterString()))
            using (var command =
                new SqlCommand(closeConnections ? Commands.Database_Drop_CloseConnections : Commands.Database_Drop,
                    connection))
            {
                command.CommandText = command.CommandText.Replace("@Database", _builder.InitialCatalog);
                await command.ExecuteNonQueryAsync();
            }
        }


        public bool Exists()
        {
            using (var connection = SqlConnectionFactory.CreateAndOpen(_builder.ToMasterString()))
            using (var command = new SqlCommand(Commands.Database_ID, connection))
            {
                command.Parameters.AddWithValue("@Database", _builder.InitialCatalog);
                var reader = command.ExecuteReader();
                return reader.Read() && !reader.IsDBNull(0);
            }
        }

        public async Task<bool> ExistsAsync()
        {
            using (var connection = await SqlConnectionFactory.CreateAndOpenAsync(_builder.ToMasterString()))
            using (var command = new SqlCommand(Commands.Database_ID, connection))
            {
                command.Parameters.AddWithValue("@Database", _builder.InitialCatalog);
                var reader = await command.ExecuteReaderAsync();
                return await reader.ReadAsync() && !await reader.IsDBNullAsync(0);
            }
        }


        public bool ExecuteReadWriteCheck(TimeSpan? timeout = null)
        {
            var waitTime = timeout ?? TimeSpan.FromMinutes(10);
            var waitStartTime = DateTime.Now;
            while (DateTime.Now.Subtract(waitStartTime).TotalSeconds < waitTime.TotalSeconds)
                try
                {
                    var validationTableName = $"ReadWriteCheck{DateTime.Now.Ticks}";

                    using (var connection = SqlConnectionFactory.CreateAndOpen(_builder))
                    using (var command = new SqlCommand(
                        $"CREATE TABLE {validationTableName}(TestID int); DROP TABLE {validationTableName};",
                        connection))
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }

            return false;
        }


        public string[] GetSchemas()
        {
            using (var connection = SqlConnectionFactory.CreateAndOpen(_builder))
            using (var command = new SqlCommand(Commands.Database_Schemas_Get_All, connection))
            {
                var results = new List<string>();
                var reader = command.ExecuteReader();
                while (reader.Read()) results.Add(reader[0].ToString());
                return results.OrderBy(s => s).ToArray();
            }
        }

        public async Task<bool> ExecuteReadWriteCheckAsync(TimeSpan? timeout = null)
        {
            var waitTime = timeout ?? TimeSpan.FromMinutes(10);
            var waitStartTime = DateTime.Now;
            while (DateTime.Now.Subtract(waitStartTime).TotalSeconds < waitTime.TotalSeconds)
                try
                {
                    var validationTableName = $"ReadWriteCheck{DateTime.Now.Ticks}";
                    using (var connection = await SqlConnectionFactory.CreateAndOpenAsync(_builder))
                    using (var command = new SqlCommand(
                        $"CREATE TABLE {validationTableName}(TestID int); DROP TABLE {validationTableName};",
                        connection))
                    {
                        await command.ExecuteNonQueryAsync();
                        return true;
                    }
                }
                catch
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }

            return false;
        }

        public override string ToString()
        {
            return $"{nameof(SqlDatabase)}[{_builder.InitialCatalog}]";
        }
    }
}