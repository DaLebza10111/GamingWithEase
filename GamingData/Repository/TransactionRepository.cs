using Dapper;
using Models.BaseModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace GamingData.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IConfiguration _config;

        public TransactionRepository(IConfiguration config)
        {
            _config = config;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_config.GetConnectionString("TransactionsDB"));
        }

        public async Task<IEnumerable<TransactionModel>> GetTransactionsByClientIdAsync(int clientId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<TransactionModel>(
                    "GetTransactionsByClientId",
                    new { ClientID = clientId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateTransactionCommentAsync(int transactionId, string comment)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(
                    "UpdateTransactionComment",
                    new { TransactionID = transactionId, Comment = comment },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task AddTransactionAsync(TransactionModel transaction)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(
                    "AddTransaction",
                    new
                    {
                        Amount = transaction.Amount,
                        TransactionTypeID = transaction.TransactionTypeID,
                        ClientID = transaction.ClientID,
                        Comment = transaction.Comment
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateClientBalanceAsync(int clientId, decimal amount, bool isCredit)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(
                    "UpdateClientBalance",
                    new
                    {
                        ClientID = clientId,
                        Amount = amount,
                        IsCredit = isCredit
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

    }
}
