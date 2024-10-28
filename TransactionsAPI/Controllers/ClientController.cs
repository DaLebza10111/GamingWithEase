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

            return Result<ClientModel>.Success(client);
        }

        [HttpGet("AddClient")]
        public async Task<Result<ClientModel>> AddClient([FromBody] ClientModel client)
        {
            var newclients = await _client.AddClientAsync(client);
            return Result<ClientModel>.Success(newclients);
        }
    }
}
