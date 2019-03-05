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
}