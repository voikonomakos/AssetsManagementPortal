using AssetsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace AssetsManagement.Pages
{
    public class ClientsModel : PageModel
    {
       

        public int? SelectedClientId { get; set; }
        public PaginatedList<Client>? Clients { get; set; }
        public List<Models.Asset>? Assets { get; set; }
        public List<Models.Currency>? Currencies { get; set; }
        public List<AccountManager>? AccountManagers { get; set; }
        public string? CurrentFilter { get; set; }
        public string? CurrentSort { get; set; }
        public string? NameSort { get; set; }
        public string? DateSort { get; set; }
        private Db db;

        public ClientsModel(Db db)
        {
            this.db = db;
        }

        public void OnGet(int? id, string? searchString, int pageIndex = 1)
        {
            CurrentFilter = searchString;
            var clientsList = db.Clients.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                clientsList = clientsList.Where(c => c.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }
            int pageSize = 5;
            Clients = new PaginatedList<Client>(clientsList.ToList(), clientsList.Count(), pageIndex, pageSize);
            Currencies = db.Currencies;
            AccountManagers = db.AccountManagers;

            if (id != null)
            {
                var selectedClient = clientsList.FirstOrDefault(c => c.Id == id);
                if (selectedClient != null)
                {
                    SelectedClientId = selectedClient.Id;
                    Assets = selectedClient.Assets;
                }
            }
        }
    }
}
