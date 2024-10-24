
using GamingData.Models;

namespace GamingData.Repository
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsByClientIdAsync(int clientId);
        Task UpdateTransactionCommentAsync(int transactionId, string comment);
        Task AddTransactionAsync(Transaction transaction);
        Task UpdateClientBalanceAsync(int clientId, decimal amount, bool isCredit);
    }
}
