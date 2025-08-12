using AssetsManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetsManagement.Pages
{
    [Authorize]
    public class ExchangeRatesModel : PageModel
    {
        public IList<ExchangeRate> ExchangeRates { get; private set; }
        private Db db;

        public ExchangeRatesModel(Db db)
        {
            this.db = db;
        }
        public void OnGet()
        {
            ExchangeRates = db.Rates.ToList();
        }
    }
}
