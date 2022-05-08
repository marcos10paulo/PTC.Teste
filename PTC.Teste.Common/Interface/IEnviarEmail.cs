namespace PTC.Teste.Common.Interface
{
    public interface IEnviarEmail
    {
        void EnviarEmail(string destinatarios, string titulo, string mensagem);
    }
}
