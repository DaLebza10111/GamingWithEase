using System.ComponentModel.DataAnnotations;

namespace GamingData.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        [Required]
        public string? ClientName { get; set; }
        [Required]
        public string? ClientSurname { get; set; }
        [Required]
        public double ClientBalance { get; set; }
    }
}
