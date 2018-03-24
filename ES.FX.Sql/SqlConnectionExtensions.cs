using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ES.FX.Sql
{
    public static class SqlConnectionExtensions
    {
        public static SqlConnection OpenConnection(this SqlConnection connection)
        {
            connection.Open();
            return connection;
        }


        public static async Task<SqlConnection> OpenConnectionAsync(this SqlConnection connection)
        {
            await connection.OpenAsync();
            return connection;
        }

        public static async Task<SqlConnection> OpenAsync(this SqlConnection connection,
            CancellationToken cancellationToken)
        {
            await connection.OpenAsync(cancellationToken);
            return connection;
        }
    }

    public static class SqlConnectionFactory
    {
        public static SqlConnection Create(SqlConnectionStringBuilder builder, string database = null)
        {
            return new SqlConnection(
                new SqlConnectionStringBuilder(builder.ToString()).SetInitialCatalog(database).ToString());
        }


        public static SqlConnection Create(string connectionString, string database = null)
        {
            return new SqlConnection(
                new SqlConnectionStringBuilder(connectionString).SetInitialCatalog(database).ToString());
        }


        public static SqlConnection CreateAndOpen(string connectionString, string database = null)
        {
            return Create(connectionString, database).OpenConnection();
        }

        public static SqlConnection CreateAndOpen(SqlConnectionStringBuilder builder, string database = null)
        {
            return Create(builder, database).OpenConnection();
        }


        public static async Task<SqlConnection> CreateAndOpenAsync(string connectionString, string database = null)
        {
            return await Create(connectionString, database).OpenConnectionAsync();
        }

        public static async Task<SqlConnection> CreateAndOpenAsync(SqlConnectionStringBuilder builder,
            string database = null)
        {
            return await Create(builder, database).OpenConnectionAsync();
        }
    }
}