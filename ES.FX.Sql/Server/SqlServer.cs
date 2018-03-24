using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ES.FX.Sql.Server
{
    public class SqlServer
    {
        private readonly SqlConnectionStringBuilder _builder;


        internal SqlServer(SqlConnectionStringBuilder builder)
        {
            _builder = builder;
        }


        public bool IsAzure()
        {
            return GetProperty("Edition").Contains("Azure");
        }

        public async Task<bool> IsAzureAsync()
        {
            return (await GetPropertyAsync("Edition")).Contains("Azure");
        }

        public string GetProperty(string propertyName)
        {
            using (var connection = SqlConnectionFactory.CreateAndOpen(_builder.ToMasterString()))
            using (var command = new SqlCommand(Commands.Server_Property_Get, connection))
            {
                command.Parameters.AddWithValue("@Property", propertyName);
                var reader = command.ExecuteReader();
                return reader.Read() ? reader[0].ToString() : null;
            }
        }

        public async Task<string> GetPropertyAsync(string propertyName)
        {
            using (var connection = await SqlConnectionFactory.CreateAndOpenAsync(_builder.ToMasterString()))
            using (var command = new SqlCommand(Commands.Server_Property_Get, connection))
            {
                command.Parameters.AddWithValue("@Property", propertyName);
                var reader = await command.ExecuteReaderAsync();
                return await reader.ReadAsync() ? reader[0].ToString() : null;
            }
        }

        public override string ToString()
        {
            return $"{nameof(SqlServer)}[{_builder}]";
        }
    }
}