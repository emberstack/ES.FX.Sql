using System.Data.SqlClient;

namespace ES.FX.Sql.Server
{
    public class SqlServerManager
    {
        public SqlServerManager(string connectionString)
        {
            Server = new SqlServer(new SqlConnectionStringBuilder(connectionString));
            Databases = new SqlDatabaseCollection(new SqlConnectionStringBuilder(connectionString));
        }


        public SqlServer Server { get; }
        public SqlDatabaseCollection Databases { get; }
    }
}