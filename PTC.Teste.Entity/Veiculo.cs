using PTC.Teste.Entity.Enum;
using System.Text.Json.Serialization;

namespace PTC.Teste.Entity
{
    public class Veiculo : EntityBase
    {
        public int ProprietarioId { get; set; }
        public string Renavam { get; set; }
        public int MarcaId { get; set; }
        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public int Quilometragem { get; set; }
        public decimal Valor { get; set; }
        public SituacaoVeiculo SituacaoVeiculo { get; set; }

        [JsonIgnore]
        public Marca Marca { get; set; }
        [JsonIgnore]
        public Proprietario Proprietario { get; set; }
    }
}
