using AssetsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetsManagement.Pages
{
    public class DeleteClientModel : PageModel
    {
        private readonly Db db;
        private readonly ILogger<DeleteClientModel> _logger;

        public DeleteClientModel(Db db, ILogger<DeleteClientModel> logger)
        {
            this.db = db;
            _logger = logger;
        }

        [BindProperty]
        public Client? Client { get; set; }
        public string? ErrorMessage { get; set; }
        public List<Currency> Currencies { get; set; } = new List<Currency>();
        public List<AccountManager> AccountManagers { get; set; } = new List<AccountManager>();
        public IActionResult OnGet(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Client = db.Clients.FirstOrDefault(m => m.Id == id);
            Currencies = db.Currencies;
            AccountManagers = db.AccountManagers;

            if (Client == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = String.Format("Delete {0} failed. Try again", id);
            }

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = db.Clients.FirstOrDefault(m => m.Id == id);
            Currencies = db.Currencies;
            AccountManagers = db.AccountManagers;

            if (client == null)
            {
                return NotFound();
            }

            try
            {
                db.Clients.Remove(client);
                return RedirectToPage("./Clients");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessage);

                return RedirectToPage("./DeleteClient", new { id, saveChangesError = true });
            }
        }
    }
}