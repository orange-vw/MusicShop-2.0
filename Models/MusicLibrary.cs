namespace MusicShop.Models
{
    public class MusicLibrary
    {
        public required int Id { get; set; }
        public string Genre { get; set; }
        public string ArtistName { get; set; }
        public required string SongName { get; set; }
        public required decimal Price { get; set; }
    }
}
