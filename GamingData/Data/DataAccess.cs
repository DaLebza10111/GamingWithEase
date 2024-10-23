using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace GamingData.Data
{
    public class DataAccess
    {
        private readonly IConfiguration _config;

        public DataAccess(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<IEnumerable<T>> GetDataListAsync<T, P>(string query, P parameters, string connectionId = "TransactionsDB")
        {
            using IDbConnection conn = new SqlConnection(_config.GetConnectionString(connectionId));

            return await conn.QueryAsync<T>(query, parameters);
        }

        public async Task SaveDataAsync<P>(string query, P parameters, string connectionId = "TransactionsDB")
        {
            using IDbConnection conn = new SqlConnection(_config.GetConnectionString(connectionId));
            await conn.ExecuteAsync(query, parameters);
        }
    }
}