using lab5.Models;
using lab5.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab5.Pages
{
    public class SummaryModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid token { get; set; }

        [BindProperty(SupportsGet = true)]
        public int randomNum { set; get; }

        public IDBServices dBServices;
        public ViewProperty CV { set; get; }

        public SummaryModel(IDBServices dbServices)
        {
            this.dBServices = dbServices;
        }
        public async Task OnGet()
        {
            CV = await dBServices.getCVSummary(token);
            return;
        }
    }
}
