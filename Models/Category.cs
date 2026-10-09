using System.ComponentModel.DataAnnotations;

namespace LibrarieOnline.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Numele categoriei este obligatoriu.")]
        [StringLength(100, ErrorMessage = "Numele categoriei nu poate depăși 100 de caractere.")]
        [Display(Name = "Categorie")]
        public string Name { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Descrierea nu poate depăși 200 de caractere.")]
        [Display(Name = "Descriere")]
        public string? Description { get; set; }
    }
}