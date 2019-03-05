using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ES.FX.Sql
{
    public static class SqlConnectionFactory
    {
        public static SqlConnection Create(SqlConnectionStringBuilder builder, string database = null)
        {
            return new SqlConnection(builder.Clone().SetInitialCatalog(database).ToString());
        }


        public static SqlConnection Create(string connectionString, string database = null)
        {
            return new SqlConnection(connectionString.ToSqlConnectionStringBuilder().SetInitialCatalog(database).ToString());
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