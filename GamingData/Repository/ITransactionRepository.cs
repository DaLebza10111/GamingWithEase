using Models.BaseModels;

namespace GamingData.Repository
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<TransactionModel>> GetTransactionsByClientIdAsync(int clientId);
        Task UpdateTransactionCommentAsync(int transactionId, string comment);
        Task AddTransactionAsync(TransactionModel transaction);
        Task UpdateClientBalanceAsync(int clientId, decimal amount, bool isCredit);
    }
}
