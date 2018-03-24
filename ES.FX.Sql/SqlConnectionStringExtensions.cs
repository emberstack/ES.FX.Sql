using System.Data.SqlClient;

namespace ES.FX.Sql
{
    /// <summary>
    ///     Collection of extension methods for <see cref="SqlConnectionStringBuilder" />
    /// </summary>
    public static class SqlConnectionStringExtensions
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SqlConnectionStringBuilder" /> from a <see cref="string" />
        /// </summary>
        public static SqlConnectionStringBuilder ToSqlConnectionStringBuilder(this string connectionString)
        {
            return new SqlConnectionStringBuilder(connectionString);
        }


        /// <summary>
        ///     Initializes a new instance of <see cref="SqlConnectionStringBuilder" /> from another instance.
        /// </summary>
        public static SqlConnectionStringBuilder Duplicate(this SqlConnectionStringBuilder source,
            string initialCatalog = null)
        {
            if (source == null) return null;
            var builder = new SqlConnectionStringBuilder(source.ConnectionString);
            builder.SetInitialCatalog(initialCatalog);
            return builder;
        }

        /// <summary>
        ///     Changes the InitialCatalog to the "master" database
        /// </summary>
        public static SqlConnectionStringBuilder SetInitialCatalogToMaster(this SqlConnectionStringBuilder builder)
        {
            return builder.SetInitialCatalog("master");
        }


        public static SqlConnectionStringBuilder SetInitialCatalog(this SqlConnectionStringBuilder builder,
            string database = null)
        {
            if (builder != null) builder.InitialCatalog = database ?? builder.InitialCatalog;
            return builder;
        }


        public static string ToMasterString(this SqlConnectionStringBuilder builder)
        {
            return builder?.Duplicate().SetInitialCatalogToMaster().ToString();
        }
    }
}