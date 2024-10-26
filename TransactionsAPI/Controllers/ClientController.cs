using Models.BaseModels;
using GamingData.Repository;
using Microsoft.AspNetCore.Mvc;

namespace TransactionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _client;

        public ClientController(IClientRepository clientRepository)
        {
            _client = clientRepository;
        }

        [HttpGet("GetClients")]
        public async Task<Result<IEnumerable<ClientModel>>> GetClients()
        {
            var clients = await _client.GetAllClientsAsync();
            return Result<IEnumerable<ClientModel>>.Success(clients);
        }

        [HttpGet("{id}")]
        public async Task<Result<ClientModel>> GetClientById(int id)
        {
            var client = await _client.GetClientByIdAsync(id);
            //if (client == null)
            //{
            //    string[] msg[0] = "Client not found";
            //    return Result<ClientModel>.Failure(msg);
            //}
            return Result<ClientModel>.Success(client);
        }
    }
}
