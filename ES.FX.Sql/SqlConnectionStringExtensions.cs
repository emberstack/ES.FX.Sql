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
        public static SqlConnectionStringBuilder Clone(this SqlConnectionStringBuilder source,
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

        /// <summary>
        /// Sets the InitialCatalog to <param name="database"></param>
        /// </summary>
        public static SqlConnectionStringBuilder SetInitialCatalog(this SqlConnectionStringBuilder builder,
            string database = null)
        {
            if (builder != null) builder.InitialCatalog = database ?? builder.InitialCatalog;
            return builder;
        }


        /// <summary>
        /// Creates a new instance of <see cref="SqlConnectionStringBuilder"/> with <see cref="SqlConnectionStringBuilder.InitialCatalog"/> set to "master"
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static SqlConnectionStringBuilder CloneForMaster(this SqlConnectionStringBuilder builder)
        {
            return builder?.Clone().SetInitialCatalogToMaster();
        }
    }
}