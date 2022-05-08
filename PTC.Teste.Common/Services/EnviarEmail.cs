using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using PTC.Teste.Common.Interface;
using PTC.Teste.Dto.Configuration;

namespace PTC.Teste.Common.Service
{
    public class EnviarEmail : IEnviarEmail
    {
        private readonly RemetenteConfiguration _remententeConfiguration;

        public EnviarEmail(IOptions<RemetenteConfiguration> option)
        {
            _remententeConfiguration = option.Value;
        }

        void IEnviarEmail.EnviarEmail(string destinatarios, string titulo, string mensagem)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_remententeConfiguration.Email)
            };

            email.To.Add(MailboxAddress.Parse(destinatarios));
            email.Subject = titulo;

            var builder = new BodyBuilder
            {
                HtmlBody = mensagem
            };

            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_remententeConfiguration.Host, _remententeConfiguration.Porta, SecureSocketOptions.StartTls);
            smtp.Authenticate(_remententeConfiguration.Email, _remententeConfiguration.Senha);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
