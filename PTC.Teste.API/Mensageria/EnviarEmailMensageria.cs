using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PTC.Teste.Dto;
using PTC.Teste.Dto.Configuration;
using RabbitMQ.Client;
using System.Text;

namespace PTC.Teste.API.Controllers
{

    public class EnviarEmailMensageria
    {
        private readonly ConnectionFactory _factory;        

        public EnviarEmailMensageria(ConnectionFactory factory)
        {
            _factory = factory;
        }

        public bool EnviarEmail(EnviarEmailVeiculoDto model)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: $"EnviarEmail",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            var stringfiedMessage = JsonConvert.SerializeObject(model);
            var bytesMessage = Encoding.UTF8.GetBytes(stringfiedMessage);

            channel.BasicPublish(
                    exchange: "",
                    routingKey: $"EnviarEmail",
                    basicProperties: null,
                    body: bytesMessage
                );

            return true;
        }
    }
}
