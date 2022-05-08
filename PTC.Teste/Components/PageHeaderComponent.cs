using PTC.Teste.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTC.Teste.Components
{
    public class PageHeaderComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string Title, Dictionary<string, string> Routes)
        {
            PageHeaderViewModel model = new()
            {
                Title = Title,
                Routes = Routes
            };

            return View(model);
        }
    }
}
