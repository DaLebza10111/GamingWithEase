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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientModel>>> GetClients()
        {
            var clients = await _client.GetAllClientsAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientModel>> GetClientById(int id)
        {
            var client = await _client.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }
    }
}
