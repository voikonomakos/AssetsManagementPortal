using AssetsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetsManagement.Pages
{
    public class DeleteAccountManagerModel : PageModel
    {
        private readonly Db db;
        private readonly ILogger<DeleteAccountManagerModel> _logger;

        public DeleteAccountManagerModel(Db db, ILogger<DeleteAccountManagerModel> logger)
        {
            this.db = db;
            _logger = logger;
        }

        [BindProperty]
        public AccountManager? AccountManager { get; set; }
        public string? ErrorMessage { get; set; }
        public List<Client>? Clients { get; set; } = new List<Client>();
        public List<Currency>? Currencies { get; set; } = new List<Currency>();

        public IActionResult OnGet(int? id, bool? saveChangesError = false)
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

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = string.Format("Delete {0} failed. Try again", id);
            }

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountmanager = db.AccountManagers.FirstOrDefault(am => am.Id == id);
            Clients = db.Clients;
            Currencies = db.Currencies;

            if (accountmanager == null)
            {
                return NotFound();
            }

            try
            {
                db.AccountManagers.Remove(accountmanager);
                return RedirectToPage("./AccountManagers");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessage);
                return RedirectToPage("./DeleteAccountManager", new { id, saveChangesError = true });
            }
        }
    }
}
