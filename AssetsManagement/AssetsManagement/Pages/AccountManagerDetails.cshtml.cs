using AssetsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace AssetsManagement.Pages
{
    public class AccountManagerDetailsModel : PageModel
    {
        private readonly Db db;

        public AccountManagerDetailsModel(Db db)
        {
            this.db = db;
        }

        public AccountManager? AccountManager { get; set; }
        public List<Client>? Clients { get; set; } = new List<Client>();
        public List<Currency>? Currencies { get; set; } = new List<Currency>();

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AccountManager = db.AccountManagers.FirstOrDefault(am => am.Id == id);
            Clients = db.Clients;
            Currencies = db.Currencies;

            if (AccountManager == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
