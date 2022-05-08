using PTC.Teste.Repository;
using System.Threading.Tasks;

namespace PTC.Teste.Api.Helpers
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string username, string password);
    }

    internal class AuthenticationService : IAuthenticationService
    {
        public async Task<bool> Authenticate(string username, string password)
        {
            var success = await Task.Run(() => new AutenticacaoRepository().Check(username, password));
            return success;
        }
    }
}
