using System;
using System.Threading.Tasks;
using ES.FX.Sql.Server.Azure;

namespace ES.FX.Sql.Server
{
    public interface ISqlDatabase
    {
        string Name { get; }
        void Create(AzureDatabaseTierDetails azureDetails = null);
        Task CreateAsync(AzureDatabaseTierDetails azureDetails = null);
        void Drop(bool closeConnections = false);
        Task DropAsync(bool closeConnections = false);
        bool Exists();
        Task<bool> ExistsAsync();
        bool ExecuteReadWriteCheck(TimeSpan? timeout = null);
        string[] GetSchemas();
    }
}