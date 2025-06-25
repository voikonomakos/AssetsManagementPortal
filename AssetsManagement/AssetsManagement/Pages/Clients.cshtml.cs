using AssetsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace AssetsManagement.Pages
{
    public class ClientsModel : PageModel
    {
        public required List<Client> Clients { get; set; }
       public List<Models.Asset>? Assets { get; set; }

        public int? SelectedClientId { get; set; }
        private Db db;

        public ClientsModel(Db db)
        {
            this.db = db;
        }

        public void OnGet(int? id)
        {
            Clients = db.Clients;
            if (id != null)
            {
                var selectedClient = Clients.FirstOrDefault(c => c.Id == id);
                if (selectedClient != null)
                {

                    SelectedClientId = selectedClient.Id;
                    Assets = selectedClient.Assets;
                }

            }
        }
    }
}
