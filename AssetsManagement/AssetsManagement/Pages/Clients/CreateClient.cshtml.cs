using AssetsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AssetsManagement.Pages.Clients
{
    public class CreateClientModel : PageModel
    {
        private readonly Db db;
        private readonly ILogger<CreateClientModel> _logger;

        public CreateClientModel(Db db, ILogger<CreateClientModel> logger)
        {
            this.db = db;
            _logger = logger;
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
            Currencies = db.Currencies;
            AccountManagers = db.AccountManagers;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            db.Clients.Add(Client);

            return RedirectToPage("Clients");
        }
    }
}