namespace GamingData.Data
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> GetDataListAsync<T, P>(string query, P parameters, string connectionId = "TransactionsDB");
        Task SaveDataAsync<P>(string query, P parameters, string connectionId = "TransactionsDB");
    }
}