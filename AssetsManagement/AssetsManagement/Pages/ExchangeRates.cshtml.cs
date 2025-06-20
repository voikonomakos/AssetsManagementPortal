using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetsManagement.Pages
{
    public class ExchangeRate
    {
        public string Currency { get; set; } = string.Empty;
        public decimal Rate { get; set; } = 0.0m;
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }

    public class ExchangeRatesModel : PageModel
    {
        public List<ExchangeRate> ExchangeRates { get; private set; }

        public void OnGet()
        {
            ExchangeRates = new List<ExchangeRate>();
            ExchangeRates.Add(new ExchangeRate { Currency = "USD", Rate = 1.0m, LastUpdated = DateTime.Now });
            ExchangeRates.Add(new ExchangeRate { Currency = "EUR", Rate = 0.85m, LastUpdated = DateTime.Now });
            ExchangeRates.Add(new ExchangeRate { Currency = "GBP", Rate = 0.75m, LastUpdated = DateTime.Now });
        }
    }
}
