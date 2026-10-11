using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarieOnline.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Titlul este obligatoriu.")]
        [StringLength(150, ErrorMessage = "Titlul nu poate depăși 150 de caractere.")]
        [Display(Name = "Titlu")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Prețul este obligatoriu.")]
        [Range(1, 10000, ErrorMessage = "Prețul trebuie să fie mai mare decât 0.")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Preț")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Autorul este obligatoriu.")]
        [Display(Name = "Autor")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Categoria este obligatorie.")]
        [Display(Name = "Categorie")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Editura este obligatorie.")]
        [Display(Name = "Editură")]
        public int PublisherId { get; set; }
        [Display(Name = "Descriere")]
        public string? Description { get; set; }

        [Display(Name = "ISBN")]
        [StringLength(13, MinimumLength = 10)]
        public string? ISBN { get; set; }

        public Author? Author { get; set; }
        public Category? Category { get; set; }
        public Publisher? Publisher { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}