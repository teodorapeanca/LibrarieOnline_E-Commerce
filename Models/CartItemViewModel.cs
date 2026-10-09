namespace LibrarieOnline.Models
{
    public class CartItemViewModel
    {
        public Book Book { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal TotalPrice => Book.Price * Quantity;
    }
}