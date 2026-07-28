using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace lab5.Models
{
 
    public class CVBindingModel
    {

        [Required]
        [StringLength(20, ErrorMessage = "Maximum length is {1}")]
        [Display(Name = "Your name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        [Display(Name = "Your last name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly Birthday { get; set; }


        [Required]
        public string Gender { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Compare("Email", ErrorMessage = "Emails do not match!")]
        public string EmailConfirmation { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IFormFile Image { set; get; }
        public List<string> Skills { get; set; } // multiselect

        public List<string> Nationalities { get; set; } // multiselect
        public int rsum { get; set; }
    }

}
