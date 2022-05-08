using PTC.Teste.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PTC.Teste.Components
{
    public class DefaultSimpleComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string controller)
        {
            var model = new DefaultSimpleViewModel()
            {
                Controller = controller
            };

            return View(model);
        }
    }
}
