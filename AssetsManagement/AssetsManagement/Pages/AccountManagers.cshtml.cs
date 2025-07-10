using AssetsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetsManagement.Pages
{
    public class AccountManagersModel : PageModel
    {
        public int? SelectedAccountManagerid { get; set; }
        public List<AssetsManagement.Models.Asset>? Assets { get; set; }
        public int? SelectedClientId { get; set; }
        public PaginatedList<AccountManager>? AccountManagers { get; set; }
        public List<Client>? Clients { get; set; }
        public List<Currency>? Currencies { get; set; }
        public string? CurrentFilter { get; set; }
        public string? CurrentSort { get; set; }
        public string? NameSort { get; set; }
        public string? DateSort { get; set; }
        private Db db;

        public AccountManagersModel(Db db)
        {
            this.db = db;
        }

        public void OnGet(int? id, string? searchString, int pageIndex = 1)
        {
            CurrentFilter = searchString;
            var accountmanagersList = db.AccountManagers.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                accountmanagersList = accountmanagersList.Where(am => am.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }
            int pageSize = 5;
            AccountManagers = new PaginatedList<AccountManager>(accountmanagersList.ToList(), accountmanagersList.Count(), pageIndex, pageSize);
            Clients = db.Clients;
            Currencies = db.Currencies;

            if (id != null)
            {
                Clients = db.Clients.Where(c => c.AccountManagerId == id).ToList();
            }

            if (Request.Query.ContainsKey("clientId") && int.TryParse(Request.Query["clientId"], out int clientId))
            {
                SelectedClientId = clientId;
                var selectedClient = Clients.FirstOrDefault(c => c.Id == SelectedClientId);

                if (selectedClient != null)
                {
                    Assets = selectedClient.Assets;
                }
                
            }
        }
    }
}