using GamingData.Models;

namespace GamingData.Repository
{
    public class ClientRepository : IClientRepository
    {
        public Task<bool> AddNewClient(Client client)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteClient(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetClientByID(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> GetClientList()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateClientDetails(Client client)
        {
            throw new NotImplementedException();
        }
    }
}