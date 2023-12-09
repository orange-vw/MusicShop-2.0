using System.ComponentModel.DataAnnotations;

namespace MusicShop.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public Dictionary<int, int>? SelectedSongs { get; set; }
        public string? CardNumber { get; set; }
        public string? Address { get; set; }
        public int PhoneNumber { get; set; }
        public string? Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
    }
}