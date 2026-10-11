
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibrarieOnline.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Numele autorului este obligatoriu.")]
        [StringLength(100, ErrorMessage = "Numele nu poate depăși 100 de caractere.")]
        [Display(Name = "Nume autor")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Naționalitatea este obligatorie.")]
        [StringLength(100, ErrorMessage = "Naționalitatea nu poate depăși 100 de caractere.")]
        [Display(Name = "Naționalitate")]
        public string? Nationality { get; set; }

        // Cărțile scrise de acest autor
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
