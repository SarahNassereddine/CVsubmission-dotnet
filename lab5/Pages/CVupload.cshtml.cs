using lab5.Models;
using lab5.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace lab5.Pages
{
    public class CVuploadModel : PageModel
    {
        IDBServices _DbServices;
        public CVuploadModel(IDBServices dBServices) {
            _DbServices = dBServices;
        }
        [BindProperty]
        public CVBindingModel Input { get; set; }
     

        public CVViewModel ViewModel { get; set; } = new CVViewModel();


        public int random1 { get; set; }
        public int random2 { get; set; }

        // NOT BindProperty — loaded from application, not from request

        public List<SelectListItem> NationalitiesOptions { get; set; } = new List<SelectListItem> {
          new SelectListItem { Value = "US", Text = "United States" },
                new SelectListItem { Value = "UK", Text = "United Kingdom" },
                new SelectListItem { Value = "CA", Text = "Canada" },
                new SelectListItem { Value = "AU", Text = "Australia" } };

        public List<string> SkillsOptions { get; set; } = new List<string> { "Java", "Python", "ASP.NET" };
        public void OnGet()
        {
            Random rnd = new Random();
            random1 = rnd.Next(1,100);
            random2 = rnd.Next(1,100);

            TempData["ExpectedSum"] = random1 + random2;
            ViewModel = new CVViewModel
            {
                rand1 = random1,
                rand2 = random2,
                Skills = new List<string>()
            };

        }
        public async Task<IActionResult> OnPost(int randomNum)
        {
            int? expectedSum = TempData["ExpectedSum"] as int?;

            if (expectedSum == null || Input.rsum != expectedSum)
            {
                ModelState.AddModelError("sum", "Incorrect captcha sum!");
            }

            if (String.IsNullOrEmpty(Input.Password) || Input.Password.Length < 8)
            {
                ModelState.AddModelError("length","Password must contain more than 8 charachters");
            }
            else if (!Input.Password.Any(char.IsLetter))
            {
                ModelState.AddModelError("no letter", "Password must contain at least one letter.");
            }
            else if (!Input.Password.Any(char.IsDigit))
            {
                ModelState.AddModelError("no digit", "Password must contain at least one digit.");
            }
            else if (!Input.Password.Any(c => !char.IsLetterOrDigit(c)))
            {
                ModelState.AddModelError("no char", "Password must contain at least one symbol.");
            }
          
            if (!ModelState.IsValid)
            {
                Random rnd = new Random();
                random1 = rnd.Next(1, 100);
                random2 = rnd.Next(1, 100);
                TempData["ExpectedSum"] = random1 + random2;

                ViewModel = new CVViewModel
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Birthday = Input.Birthday,
                    Nationalities = Input.Nationalities,
                    Gender = Input.Gender,
                    Skills = Input.Skills ?? new List<string>(),
                    Email =Input.Email,
                    EmailConfirmation = Input.EmailConfirmation,
                    Password = Input.Password,
                    rand1 = random1,
                    rand2 = random2
                   
                };
                return Page();  // Returns PageResult
            }

            Guid token = await  _DbServices.saveCV(Input);
         
            return RedirectToPage("/Summary", new {  randomNum, token });
        }
    }
}
