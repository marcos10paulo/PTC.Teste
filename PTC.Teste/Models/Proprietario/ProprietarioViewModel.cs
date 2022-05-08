using PTC.Teste.CustomValidation;
using PTC.Teste.Entity.Enum;
using PTC.Teste.Properties;
using System.ComponentModel.DataAnnotations;

namespace PTC.Teste.Models.Proprietario
{
    public class ProprietarioViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public string Nome { get; set; }
        [Display(Name = "Documento")]
        [ValidarCPF(ErrorMessage = "Documento inválido!")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public string Documento { get; set; }
        [Display(Name = "Email")]
        [ValidarEmail(ErrorMessage = "Email inválido!")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public string Email { get; set; }
        [Display(Name = "CEP")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public string Cep { get; set; }
        [Display(Name = "Endereço")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public string Endereco { get; set; }
        [Display(Name = "Nº")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public string Numero { get; set; }
        [Display(Name = "Bairro")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public string Bairro { get; set; }
        [Display(Name = "Cidade")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public string Cidade { get; set; }
        [Display(Name = "Estado")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public string Estado { get; set; }
        [Display(Name = "Status")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public Situacao Status { get; set; }

    }
}
