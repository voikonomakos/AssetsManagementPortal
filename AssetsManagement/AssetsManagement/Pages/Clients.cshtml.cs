using AssetsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetsManagement.Pages
{
    public class ClientsModel : PageModel
    {
        public List<Client> Clients { get; set; }
        public List<Asset> Assets { get; set; }
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

            }
        }
    }
}
