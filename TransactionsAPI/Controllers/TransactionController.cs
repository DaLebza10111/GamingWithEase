﻿using Models.BaseModels;
using GamingData.Repository;
using Microsoft.AspNetCore.Mvc;

namespace TransactionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> GetTransactionsByClientId(int clientId)
        {
            var transactions = await _transactionRepository.GetTransactionsByClientIdAsync(clientId);
            return Ok(transactions);
        }

        [HttpPut("{transactionId}")]
        public async Task<IActionResult> UpdateTransactionComment(int transactionId, [FromBody] string comment)
        {
            await _transactionRepository.UpdateTransactionCommentAsync(transactionId, comment);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionModel transaction)
        {
            await _transactionRepository.AddTransactionAsync(transaction);
            return Ok();
        }
    }
}
