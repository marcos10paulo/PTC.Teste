using PTC.Teste.Entity.Enum;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PTC.Teste.Entity
{
    public class Marca : EntityBase
    {
        public Marca()
        {
            Veiculo = new HashSet<Veiculo>();
        }

        public string Nome { get; set; }
        public Situacao Situacao { get; set; }

        [JsonIgnore]
        public virtual ICollection<Veiculo> Veiculo { get; set; }
    }
}
