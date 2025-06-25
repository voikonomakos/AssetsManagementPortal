using AssetsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace AssetsManagement.Pages
{
    public class ClientsModel : PageModel
    {
        public List<Client> Clients { get; set; }
        public List<Asset> Assets { get; set; }
        public int? SelectedClientId { get; set; }
        private Db db;

        public ClientsModel(Db db)
        {
            this.db = db;
        }

        public void OnGet(int? id)
        {
            Clients = db.Clients;
            if(id != null)
            {
                var selectedClient = Clients.FirstOrDefault(c => c.Id == id);
                if(selectedClient != null)
                {
                    Assets = selectedClient.Assets;
                    SelectedClientId = selectedClient.Id;
                }

            }
        }
    }
}
