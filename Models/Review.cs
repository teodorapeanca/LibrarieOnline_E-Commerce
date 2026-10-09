using System.ComponentModel.DataAnnotations;

namespace LibrarieOnline.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Comment { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Rating { get; set; }

        public int BookId { get; set; }

        public Book? Book { get; set; }
    }
}