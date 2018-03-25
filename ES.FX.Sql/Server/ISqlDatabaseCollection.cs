using System.Threading.Tasks;

namespace ES.FX.Sql.Server
{
    public interface ISqlDatabaseCollection
    {
        ISqlDatabase this[string database] { get; }
        ISqlDatabase Current { get; }
        string[] GetAll();
        Task<string[]> GetAllAsync();
    }
}