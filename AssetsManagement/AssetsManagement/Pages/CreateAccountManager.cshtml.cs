using AssetsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AssetsManagement.Pages
{
    public class CreateAccountManagerModel : PageModel
    {

        private readonly Db db;
        private readonly ILogger<CreateAccountManagerModel> _logger;

        public CreateAccountManagerModel(Db db, ILogger<CreateAccountManagerModel> logger)
        {
            this.db = db;
            _logger = logger;
        }

        [BindProperty]
        public AccountManager AccountManager { get; set; } = new AccountManager();

        public List<Currency> Currencies { get; set; } = new List<Currency>();
        public List<Client> Clients { get; set; } = new List<Client>();

        public void OnGet()
        {
            Currencies = db.Currencies;
            Clients = db.Clients;
        }

        public IActionResult OnPost()
        {
            Currencies = db.Currencies;
            Clients = db.Clients;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            int nextId = db.AccountManagers.Any() ? db.AccountManagers.Max(am => am.Id) + 1 : 1;
            AccountManager.Id = nextId;

            db.AccountManagers.Add(AccountManager);

            return RedirectToPage("AccountManagers");
        }
    }
}
