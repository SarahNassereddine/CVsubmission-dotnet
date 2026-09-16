using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace lab5.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _config;

        public LoginModel(IConfiguration config)
        {
            _config = config;
        }

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            // For a real app, move these to appsettings.json under "AdminUser".
            // Demo defaults are fine for a portfolio project.
            var adminUser = _config["AdminUser:Username"] ?? "admin";
            var adminPass = _config["AdminUser:Password"] ?? "Admin123!";

            if (Username == adminUser && Password == adminPass)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, Username) };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return RedirectToPage("/AllCvs");
            }

            ErrorMessage = "Invalid username or password.";
            return Page();
        }
    }
}
