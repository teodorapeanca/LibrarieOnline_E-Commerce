
#nullable disable

using LibrarieOnline.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace LibrarieOnline.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(
                100,
                ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare(
                "Password",
                ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            ExternalLogins =
                (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins =
                (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // Verificam daca adresa de email este deja folosita.
                var emailNormalizat = _userManager.NormalizeEmail(Input.Email.Trim());

                bool emailExistent = await _userManager.Users
                    .AnyAsync(u => u.NormalizedEmail == emailNormalizat);

                if (emailExistent)
                {
                    ModelState.AddModelError(
                        "Input.Email",
                        "Exista deja un cont inregistrat cu aceasta adresa de email.");

                    return Page();
                }

                var user = CreateUser();

                user.ProfileImagePath = "/images/profiles/default.png";

                await _userStore.SetUserNameAsync(
                    user,
                    Input.Email,
                    CancellationToken.None);

                await _emailStore.SetEmailAsync(
                    user,
                    Input.Email,
                    CancellationToken.None);

                var result =
                    await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    // Fiecare cont nou primeste rolul User.
                    var roleResult =
                        await _userManager.AddToRoleAsync(user, "User");

                    if (!roleResult.Succeeded)
                    {
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError(
                                string.Empty,
                                error.Description);
                        }

                        return Page();
                    }

                    _logger.LogInformation(
                        "A fost creat un cont nou cu rolul User.");

                    // Nu trimitem email de confirmare.
                    // Utilizatorul se autentifica imediat.
                    await _signInManager.SignInAsync(
                        user,
                        isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        error.Description);
                }
            }

            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException(
                    $"Can't create an instance of '{nameof(ApplicationUser)}'.");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException(
                    "The default UI requires a user store with email support.");
            }

            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
