using Microsoft.EntityFrameworkCore;
using MusicShop.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class MusicLibrary
    {
        public int Id { get; set; }
        public required string Genre { get; set; }
        public required string ArtistName { get; set; }
        public required string SongName { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public required decimal Price { get; set; }
    }
}
