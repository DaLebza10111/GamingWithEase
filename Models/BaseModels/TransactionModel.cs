
namespace Models.BaseModels
{
    public class TransactionModel
    {
        public int TransactionID { get; set; }
        public decimal Amount { get; set; }
        public int TransactionTypeID { get; set; }
        public int ClientID { get; set; }
        public string? Comment { get; set; }
    }
}
