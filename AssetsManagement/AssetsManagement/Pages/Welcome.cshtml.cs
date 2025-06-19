using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetsManagement.Pages
{

    public class Asset
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class WelcomeModel : PageModel
    {
        public string Message { get; set; } = string.Empty;
        public List<Asset> Assets { get; private set; }

        public void OnGet()
        {
            Assets = new List<Asset>
            {
                new Asset { Name = "Laptop", Description = "Dell XPS 13" },
                new Asset { Name = "Monitor", Description = "LG UltraFine 4K" },
                new Asset { Name = "Keyboard", Description = "Logitech MX Keys" }
            };
        }
    }
}
