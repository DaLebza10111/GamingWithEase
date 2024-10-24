using Dapper;
using GamingData.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingData.Repository
{
    public class TransactionRepository
    {
        private readonly IConfiguration _config;

        public TransactionRepository(IConfiguration config)
        {
            _config = config;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByClientIdAsync(int clientId)
        {
            var query = "SELECT * FROM [internal].[Transactions] WHERE ClientID = @ClientID";

            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<Transaction>(query, new { ClientID = clientId });
            }
        }

        public async Task UpdateTransactionCommentAsync(int transactionId, string comment)
        {
            var query = "UPDATE [internal].[Transactions] SET Comment = @Comment WHERE TransactionID = @TransactionID";

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Comment = comment, TransactionID = transactionId });
            }
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            var query = @"
            INSERT INTO [internal].[Transactions] (Amount, TransactionTypeID, ClientID, Comment)
            VALUES (@Amount, @TransactionTypeID, @ClientID, @Comment);
            UPDATE [internal].[Client] SET Balance = Balance + @Amount WHERE ClientID = @ClientID;
        ";

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(query, transaction);
            }
        }

        public async Task UpdateClientBalanceAsync(int clientId, decimal amount, bool isCredit)
        {
            var balanceQuery = isCredit
                ? "UPDATE [internal].[Client] SET Balance = Balance + @Amount WHERE ClientID = @ClientID"
                : "UPDATE [internal].[Client] SET Balance = Balance - @Amount WHERE ClientID = @ClientID";

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(balanceQuery, new { Amount = amount, ClientID = clientId });
            }
        }
    }
}
