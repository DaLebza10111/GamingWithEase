
namespace GamingData.Models
{
    public class TransactionType
    {
        public int TransactionTypeID { get; set; }
        public string? TransactionTypeName { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }

}