using PTC.Teste.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PTC.Teste.Components
{
    public class DefaultAddOrEditComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string controller, int id)
        {
            var model = new DefaultAddOrEditViewModel()
            {
                Controller = controller,
                Id = id
            };

            return View(model);
        }
    }
}
