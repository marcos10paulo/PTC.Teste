using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PTC.Teste.Common.Interface;
using PTC.Teste.Dto;
using PTC.Teste.Dto.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PTC.Teste.API.Consumers
{
    public class EnviarEmailConsumer : BackgroundService
    {
        public readonly RabbitMqConfiguration _configuration;
        public readonly IConnection _connection;
        public readonly IModel _channel;
        public readonly IServiceProvider _serviceProvider;

        public EnviarEmailConsumer(IOptions<RabbitMqConfiguration> option, IServiceProvider serviceProvider)
        {
            _configuration = option.Value;
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = _configuration.Host
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                    queue: "EnviarEmail",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var message = JsonConvert.DeserializeObject<EnviarEmailVeiculoDto>(contentString);

                EnviarEmail(message);

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume("EnviarEmail", false, consumer);

            return Task.CompletedTask;

        }

        public void EnviarEmail(EnviarEmailVeiculoDto model)
        {
            using var scope = _serviceProvider.CreateScope();
            var enviarEmailService = scope.ServiceProvider.GetRequiredService<IEnviarEmail>();

            string mensagem = @$"
Bom dia {model.Proprietario}

Seu veículo {model.Marca} {model.Modelo} foi cadastrado com sucesso!
";
            enviarEmailService.EnviarEmail(model.Email, "Cadastro de Veículo", mensagem);
        }
    }
}
