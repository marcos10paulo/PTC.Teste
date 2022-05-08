namespace PTC.Teste.Dto
{
    public class EnviarEmailVeiculoDto
    {
        public int VeiculoId { get; set; }
        public string Proprietario { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Email { get; set; }
    }
}
