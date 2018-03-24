using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ES.FX.Primitives;

namespace ES.FX.Sql.Server
{
    public class SqlDatabaseCollection
    {
        private readonly SqlConnectionStringBuilder _builder;

        public SqlDatabaseCollection(SqlConnectionStringBuilder builder)
        {
            _builder = builder;
            if (!builder.InitialCatalog.IsNullOrWhiteSpace()) CurrentDatabase = this[builder.InitialCatalog];
        }

        public SqlDatabase this[string database]
        {
            get
            {
                if (database.IsNullOrWhiteSpace())
                    throw new ArgumentNullException(nameof(database), "Database name cannot be null or whitespace.");
                return new SqlDatabase(_builder.Duplicate().SetInitialCatalog(database));
            }
        }

        public SqlDatabase CurrentDatabase { get; }

        public string[] GetAll()
        {
            using (var connection = SqlConnectionFactory.CreateAndOpen(_builder.ToMasterString()))
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
            using (var connection = await SqlConnectionFactory.CreateAndOpenAsync(_builder.ToMasterString()))
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