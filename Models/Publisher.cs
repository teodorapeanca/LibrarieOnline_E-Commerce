using System.ComponentModel.DataAnnotations;

namespace LibrarieOnline.Models
{
    public class Publisher
    {
        public int PublisherId { get; set; }

        [Required(ErrorMessage = "Numele editurii este obligatoriu.")]
        [StringLength(100, ErrorMessage = "Numele nu poate depăși 100 de caractere.")]
        [Display(Name = "Editură")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Țara este obligatorie.")]
        [StringLength(100, ErrorMessage = "Țara nu poate depăși 100 de caractere.")]
        [Display(Name = "Țară")]
        public string Country { get; set; } = string.Empty;
    }
}