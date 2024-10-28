using Models.BaseModels;

namespace GamingData.Repository
{
    public interface IClientRepository
    {
        Task<IEnumerable<ClientModel>> GetAllClientsAsync();
        Task<ClientModel> GetClientByIdAsync(int id);
        Task<ClientModel> AddClientAsync(ClientModel client);
    }
}