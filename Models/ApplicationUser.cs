using Microsoft.AspNetCore.Identity;

namespace LibrarieOnline.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? ProfileImagePath { get; set; }
    }
}