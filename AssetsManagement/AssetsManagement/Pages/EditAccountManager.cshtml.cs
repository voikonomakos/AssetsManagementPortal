using AssetsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AssetsManagement.Pages
{
    public class EditAccountManagerModel : PageModel
    {
        private readonly Db db;
        private readonly ILogger<EditAccountManagerModel> _logger;

        public EditAccountManagerModel(Db db, ILogger<EditAccountManagerModel> logger)
        {
            this.db = db;
            _logger = logger;
        }

        [BindProperty]
        public AccountManager? AccountManager { get; set; } = new AccountManager();
        public List<Currency> Currencies { get; set; } = new List<Currency>();
        public List<Client> Clients { get; set; } = new List<Client>();

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AccountManager = db.AccountManagers.FirstOrDefault(am => am.Id == id);
            if (AccountManager == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Currencies = db.Currencies;
            Clients = db.Clients;

            var managerToUpdate = db.AccountManagers.FirstOrDefault(am => am.Id == id);

            if (managerToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(managerToUpdate, "AccountManager", am => am.Name, am => am.Surname))
            {
                return RedirectToPage("AccountManagers");
            }

            return Page();
        }
    }
}
