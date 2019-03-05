using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ES.FX.Sql.Server
{
    internal class SqlDatabaseCollection : ISqlDatabaseCollection
    {
        private readonly SqlConnectionStringBuilder _builder;

        internal SqlDatabaseCollection(SqlConnectionStringBuilder builder)
        {
            _builder = builder;
            if (!string.IsNullOrWhiteSpace(builder.InitialCatalog)) Current = this[builder.InitialCatalog];
        }

        public ISqlDatabase this[string database]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(database))
                    throw new ArgumentNullException(nameof(database), "Database name cannot be null or whitespace.");
                return new SqlDatabase(_builder.CloneForMaster());
            }
        }

        public ISqlDatabase Current { get; }

        public string[] GetAll()
        {
            using (var connection = SqlConnectionFactory.CreateAndOpen(_builder.CloneForMaster().ConnectionString))
            using (var command = new SqlCommand(Commands.Database_Get_All, connection))
            {
                var result = new List<string>();
                var reader = command.ExecuteReader();
                while (reader.Read()) result.Add(reader.GetString(0));
                return result.OrderBy(s => s).ToArray();
            }
        }

        public async Task<string[]> GetAllAsync()
        {
            using (var connection = await SqlConnectionFactory.CreateAndOpenAsync(_builder.CloneForMaster().ConnectionString))
            using (var command = new SqlCommand(Commands.Database_Get_All, connection))
            {
                var result = new List<string>();
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync()) result.Add(reader.GetString(0));
                return result.OrderBy(s => s).ToArray();
            }
        }
    }
}