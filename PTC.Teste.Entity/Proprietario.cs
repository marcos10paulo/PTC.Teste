using PTC.Teste.Entity.Enum;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PTC.Teste.Entity
{
    public class Proprietario : EntityBase
    {
        public Proprietario()
        {
            Veiculo = new HashSet<Veiculo>();
        }

        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public Situacao Status { get; set; }

        [JsonIgnore]
        public virtual ICollection<Veiculo> Veiculo { get; set; }
    }
}
