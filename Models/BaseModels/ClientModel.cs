
namespace Models.BaseModels
{
    public class ClientModel
    {
        public int ClientID { get; set; }
        public string? Fullname { get; set; }
        public string? Surname { get; set; }
        public decimal ClientBalance { get; set; }
    }
}
