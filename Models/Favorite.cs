namespace LibrarieOnline.Models
{
    public class Favorite
    {
        public int FavoriteId { get; set; }

        public int BookId { get; set; }

        public Book? Book { get; set; }

        // ID-ul contului caruia ii apartine favorita
        public string? UserId { get; set; }

        // Legatura cu utilizatorul din ASP.NET Identity
        public ApplicationUser? User { get; set; }
    }
}