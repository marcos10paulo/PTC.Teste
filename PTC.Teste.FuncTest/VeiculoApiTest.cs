using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTC.Teste.API;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PTC.Teste.TestFunc
{
    public class VeiculoApiTest
    {
        private readonly HttpClient _client;

        public VeiculoApiTest()
        {
            TestServer server = new(new WebHostBuilder()
                .UseEnvironment("Production")
                .UseStartup<Startup>()
                );
            _client = server.CreateClient();

        }

        [Theory]
        [InlineData("GET")]
        public async Task VeiculoGetAllTest(string method)
        {
            HttpRequestMessage request = new(new HttpMethod(method), "/api/veiculo/selecionartodos");
            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equals(HttpStatusCode.OK, response.StatusCode);

        }


    }
}
