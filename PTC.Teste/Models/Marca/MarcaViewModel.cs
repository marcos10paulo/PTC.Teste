using PTC.Teste.Entity.Enum;
using PTC.Teste.Properties;
using System.ComponentModel.DataAnnotations;

namespace PTC.Teste.Models.Marca
{
    public class MarcaViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public string Nome { get; set; }
        [Display(Name = "Status")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public Situacao Situacao { get; set; }
    }
}
