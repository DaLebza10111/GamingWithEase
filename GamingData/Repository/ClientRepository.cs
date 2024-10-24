using GamingData.Data;
using GamingData.Models;
using System.Net.Http.Headers;

namespace GamingData.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDataAccess _db;

        public ClientRepository(IDataAccess access)
        {
            _db = access;
        }
        public async Task<bool> AddNewClient(Client client)
        {
            try
            {
                string query = "INSERT INTO [internal].[Client](Name, Surname, ClientBalance) VALUES(@ClientName, @ClientSurname, @ClientBalance)";
                await _db.SaveDataAsync(query, new { Name = client.ClientName, Surname = client.ClientSurname, Balance = client.ClientBalance });

                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        public async Task<bool> DeleteClient(int ID)
        {
            try
            {
                string query = "DELETE FROM WHERE ClientID=@ID";
                await _db.SaveDataAsync(query, new { ID = ID });
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<Client> GetClientByID(int ID)
        {
            string query = "SELECT * FROM [internal].[Client] WHERE ClientID =@ID";
            IEnumerable<Client> ClientList = await _db.GetDataListAsync<Client, dynamic>(query, new { });
            return ClientList?.FirstOrDefault();
        }

        public async Task<IEnumerable<Client>> GetClientList()
        {
            string query = "SELECT * FROM [internal].[Client]";
            var ClientList = await _db.GetDataListAsync<Client, dynamic>(query, new { });
            return ClientList;
        }

        public async Task<bool> UpdateClientDetails(Client client)
        {
            try
            {
                string query = "UPDATE [internal].[Client] SET Name=@ClientName, Surname=@ClientSurname, ClientBalance=@ClientBalance";
                await _db.SaveDataAsync(query, client);

                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }
    }
}