using AssetsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetsManagement.Pages
{
    public class CreateClientModel : PageModel
    {
        private readonly Db db;

        public CreateClientModel(Db db)
        {
            this.db = db;
        }

        [BindProperty]
        public Client Client { get; set; } = new Client();

        public List<Currency> Currencies { get; set; } = new List<Currency>();
        public List<AccountManager> AccountManagers { get; set; } = new List<AccountManager>();

        public void OnGet()
        {
            Currencies = db.Currencies;
            AccountManagers = db.AccountManagers;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Currencies = db.Currencies;
                AccountManagers = db.AccountManagers;
                return Page();
            }

            db.Clients.Add(Client);
            // Save changes to your database here if needed

            return RedirectToPage("Clients");
        }
    }
}