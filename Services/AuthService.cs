using LibrarieOnline.Models;
using Microsoft.AspNetCore.Identity;

namespace LibrarieOnline.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> Register(string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                ProfileImagePath = "/images/profiles/default.png"
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result;
        }

        public async Task<bool> Login(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(
                email,
                password,
                false,
                false);

            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}