using blog.Repository;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace blog.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<bool> SendMail(string to, string subject, string body)
        {
            var Username = "contact.negublog@gmail.com";
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("NEGU Blog", Username));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;


            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, true ? SecureSocketOptions.StartTls : SecureSocketOptions.None);
            await client.AuthenticateAsync(Username, "ejdbxnpzfxkrbkbc");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            return true;
        }
    }
}