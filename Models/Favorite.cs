namespace LibrarieOnline.Models
{
    public class Favorite
    {
        public int FavoriteId { get; set; }

        public int BookId { get; set; }

        public Book? Book { get; set; }
    }
}