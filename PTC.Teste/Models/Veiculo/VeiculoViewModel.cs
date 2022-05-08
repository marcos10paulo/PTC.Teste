using PTC.Teste.Entity.Enum;
using PTC.Teste.Models.Marca;
using PTC.Teste.Models.Proprietario;
using PTC.Teste.Properties;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PTC.Teste.Models.Veiculo
{
    public class VeiculoViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Proprietário")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public int ProprietarioId { get; set; }
        [Display(Name = "Renavam")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public string Renavam { get; set; }
        [Display(Name = "Marca")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public int MarcaId { get; set; }
        [Display(Name = "Modelo")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public string Modelo { get; set; }
        [Display(Name = "Ano Fabricação")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public int? AnoFabricacao { get; set; }
        [Display(Name = "Ano Modelo")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public int? AnoModelo { get; set; }
        [Display(Name = "Quilometragem")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public int? Quilometragem { get; set; }
        [Display(Name = "Valor")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public decimal? Valor { get; set; }
        [Display(Name = "Status")]
        [Required(ErrorMessageResourceName = nameof(Resources.Required), ErrorMessageResourceType = typeof(Resources))]
        public SituacaoVeiculo SituacaoVeiculo { get; set; }

        public string Marca { get; set; }
        public string Proprietario { get; set; }

        public List<ProprietarioViewModel> Proprietarios { get; set; }
        public List<MarcaViewModel> Marcas { get; set; }
    }
}
