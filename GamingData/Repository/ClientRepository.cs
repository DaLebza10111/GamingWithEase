using Dapper;
using GamingData.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace GamingData.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IConfiguration _config;

        public ClientRepository(IConfiguration config)
        {
            _config = config;
        }
        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_config.GetConnectionString("TransactionsDB"));
        }
        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            var query = "SELECT * FROM [internal].[Client]";

            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<Client>(query);
            }
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            var query = "SELECT * FROM [internal].[Client] WHERE ClientID = @ClientID";

            using (var connection = CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Client>(query, new { ClientID = id });
            }
        }
    }
}