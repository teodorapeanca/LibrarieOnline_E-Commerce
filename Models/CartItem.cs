
namespace LibrarieOnline.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        // Utilizatorul caruia ii apartine produsul
        public string UserId { get; set; } = string.Empty;

        // Cartea adaugata in cos
        public int BookId { get; set; }

        // Cantitatea din cos
        public int Quantity { get; set; } = 1;

        // Relatii cu utilizatorul si cartea
        public ApplicationUser? User { get; set; }

        public Book? Book { get; set; }
    }
}
