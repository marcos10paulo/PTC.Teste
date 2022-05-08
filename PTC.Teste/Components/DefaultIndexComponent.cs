using PTC.Teste.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PTC.Teste.Components
{
    public class DefaultIndexComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string controller, bool EnablePrint = false)
        {
            var model = new DefaultIndexViewModel()
            {
                Controller = controller,
                EnablePrint = EnablePrint
            };

            return View(model);
        }
    }
}
