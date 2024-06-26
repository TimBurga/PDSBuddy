using System.Net;
using System.Net.Mail;

namespace PDSBuddy;

public class MailService
{
    public async Task Send(string message)
    {
        if (!Config.NOTIFICATIONS_ENABLED)
        {
            return;
        }

        var mailMessage = new MailMessage
        {
            From = new MailAddress(Config.NOTIFICATIONS_FROM_ADDRESS),
            Subject = Config.NOTIFICATIONS_SUBJECT,
            IsBodyHtml = false,
            Body = message
        };

        mailMessage.To.Add(Config.NOTIFICATIONS_TO_ADDRESS);

        var client = new SmtpClient(Config.NOTIFICATIONS_SERVER, Config.NOTIFICATIONS_SERVER_PORT)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(Config.NOTIFICATIONS_SERVER_USER, Config.NOTIFICATIONS_SERVER_PASSWORD)
        };

        await client.SendMailAsync(mailMessage);
    }
}
