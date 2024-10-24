using System.ComponentModel.DataAnnotations;

namespace GamingData.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string? ClientName { get; set; }
        public string? ClientSurname { get; set; }
        public double ClientBalance { get; set; }
    }

}