using System.Threading.Tasks;

namespace ES.FX.Sql.Server
{
    public interface ISqlServer
    {
        bool IsAzure();
        Task<bool> IsAzureAsync();
        string GetProperty(string propertyName);
        Task<string> GetPropertyAsync(string propertyName);
    }
}