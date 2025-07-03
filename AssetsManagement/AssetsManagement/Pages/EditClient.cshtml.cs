using AssetsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AssetsManagement.Pages
{
    public class EditClientModel : PageModel
    {
        private readonly Db db;
        private readonly ILogger<EditClientModel> _logger;

        public EditClientModel(Db db, ILogger<EditClientModel> logger)
        {
            this.db = db;
            _logger = logger;
        }

        [BindProperty]
        public Client? Client { get; set; } = new Client();

        public List<Currency> Currencies { get; set; } = new List<Currency>();
        public List<AccountManager> AccountManagers { get; set; } = new List<AccountManager>();

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Client = db.Clients.FirstOrDefault(c => c.Id == id);
            Currencies = db.Currencies;
            AccountManagers = db.AccountManagers;

            if (Client == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Currencies = db.Currencies;
            AccountManagers = db.AccountManagers;

            var clientToUpdate = db.Clients.FirstOrDefault(c => c.Id == id);

            if (clientToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Client>(
                clientToUpdate,
                "Client",
                c => c.Name, c => c.LastName, c => c.CurrenciesId, c => c.AccountManagerId))
            {
                return RedirectToPage("Clients");
            }

            return Page();
        }
    }
}