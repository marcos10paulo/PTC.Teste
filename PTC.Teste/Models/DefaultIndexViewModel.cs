using PTC.Teste.Properties;
using System.ComponentModel.DataAnnotations;

namespace PTC.Teste.Models
{
    public class DefaultIndexViewModel
    {
        public string Controller { get; set; }
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public string Filtro { get; set; }
        public bool EnablePrint { get; set; }
    }
}
