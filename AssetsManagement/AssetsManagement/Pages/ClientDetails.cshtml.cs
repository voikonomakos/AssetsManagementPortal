using AssetsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace AssetsManagement.Pages
{
    public class ClientDetailsModel : PageModel
    {
        private readonly Db db;

        public ClientDetailsModel(Db db)
        {
            this.db = db;
        }

        public Client? Client { get; set; }
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
    }
}