using GamingData.Models;

namespace GamingData.Repository
{
    public interface IClientRepository
    {
        Task<bool> AddNewClient(Client client);
        Task<bool> UpdateClientDetails(Client client);
        Task<Client> GetClientByID(int ID);
        Task<bool> DeleteClient(int ID);
        Task<IEnumerable<Client>> GetClientList();
    }
}