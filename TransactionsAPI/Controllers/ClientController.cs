using GamingData.Models;
using GamingData.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransactionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _client;

        public ClientController(IClientRepository client)
        {
            _client = client;
        }
        // GET: api/<ClientController>
        [HttpGet("GetClients")]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _client.GetClientList();
            return Ok(clients);
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientByID(int id)
        {
            var client = await _client.GetClientByID(id);
            if (client is null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // POST api/<ClientController>
        [HttpPost]
        public async Task<IActionResult> PostClientAsync(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data!");
            }

            var result = await _client.AddNewClient(client);

            if (!result)
            {
                return BadRequest("Unable to save client");
            }
            return Ok(result);
        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClientDetails(int id, [FromBody] Client newclient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data!");
            }

            var clientresult = _client.GetClientByID(id);

            if (clientresult is null)
            {
                return NotFound();
            }

            newclient.ClientId = id;
            var result = await _client.UpdateClientDetails(newclient);

            if (!result)
            {
                return BadRequest("Unable to save client");
            }
            return Ok(result);
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var clientresult = _client.GetClientByID(id);

            if (clientresult is null)
            {
                return NotFound();
            }

            var result = await _client.DeleteClient(id);

            if (!result)
            {
                return BadRequest("Unable to save client");
            }
            return Ok(result);
        }
    }
}
