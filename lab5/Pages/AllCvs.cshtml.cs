using lab5.Models;
using lab5.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab5.Pages
{
    [Authorize]
    public class AllCvsModel : PageModel
    {
        readonly IDBServices _dbServices;

        public AllCvsModel(IDBServices dbServices)
        {
            _dbServices = dbServices;
        }

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public List<ViewProperty> Cvs { get; set; } = new List<ViewProperty>();

        public async Task OnGet()
        {
            Cvs = await _dbServices.getAllCVs(Search);
        }
    }
}
