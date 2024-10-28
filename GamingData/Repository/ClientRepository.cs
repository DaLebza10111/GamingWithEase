using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models.BaseModels;

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
        public async Task<IEnumerable<ClientModel>> GetAllClientsAsync()
        {
            var query = "SELECT [ClientID],[Name] AS Fullname,[Surname],[ClientBalance] FROM [Client]";

            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<ClientModel>(query);
            }
        }

        public async Task<ClientModel> GetClientByIdAsync(int id)
        {
            var query = "SELECT [ClientID],[Name] AS Fullname,[Surname],[ClientBalance] FROM [Client] WHERE ClientID = @ClientID";

            using (var connection = CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<ClientModel>(query, new { ClientID = id });
            }
        }

        public async Task<ClientModel> AddClientAsync(ClientModel client)
        {
            var query = "INSERT INTO [Client]([Name] AS Fullname,[Surname],[ClientBalance]) VALUES(@Fullname, @Surname, @ClientBalance)";

            using (var connection = CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<ClientModel>(query);
            }
        }
    }
}
