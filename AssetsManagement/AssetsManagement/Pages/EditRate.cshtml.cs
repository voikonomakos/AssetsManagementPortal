using AssetsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetsManagement.Pages
{
    public class EditRateModel : PageModel
    {
        [BindProperty]
        public ExchangeRate Rate { get; set; }
        private Db db;
        
        public EditRateModel(Db db)
        {
            this.db = db;
        }

        public void OnGet(string currency)
        {
            Rate = db.GetRate(currency);
        }

        public IActionResult OnPost(ExchangeRate rate)
        {
            db.UpdateRate(rate.Currency, rate.Value);
            return RedirectToPage("./ExchangeRates");

        }
    }
}
