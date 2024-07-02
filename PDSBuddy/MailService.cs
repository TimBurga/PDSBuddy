using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using PDSBuddy.Models;

namespace PDSBuddy;

public class MailService
{
    private readonly NotificationsLevel? NotifyLevel;
    private readonly string? FromAddress;
    private readonly string? ToAddress;
    private readonly string? NotifySubject;
    private readonly string? NotifyServer;
    private readonly int NotifyPort;
    private readonly string? NotifyUser;
    private readonly string? NotifyPassword;

    public MailService(IConfiguration config)
    {
        NotifyLevel = config.GetValue<NotificationsLevel>("NOTIFICATIONS_LEVEL");
        FromAddress = config.GetValue<string>("NOTIFICATIONS_FROM_ADDRESS");
        ToAddress = config.GetValue<string>("NOTIFICATIONS_TO_ADDRESS");
        NotifySubject = config.GetValue<string>("NOTIFICATIONS_SUBJECT");
        NotifyServer = config.GetValue<string>("NOTIFICATIONS_SERVER");
        NotifyPort = config.GetValue<int>("NOTIFICATIONS_SERVER_PORT");
        NotifyUser = config.GetValue<string>("NOTIFICATIONS_SERVER_USER");
        NotifyPassword = config.GetValue<string>("NOTIFICATIONS_SERVER_PASSWORD");
    }

    public async Task SendError(string message)
    {
        if (NotifyLevel >= NotificationsLevel.Errors)
        {
            await Send(message);
        }
    }

    public async Task Send(string message)
    {
        if (NotifyLevel == NotificationsLevel.Off)
        {
            return;
        }

        var mailMessage = new MailMessage
        {
            From = new MailAddress(FromAddress),
            Subject = NotifySubject,
            IsBodyHtml = false,
            Body = message
        };

        mailMessage.To.Add(ToAddress);

        var client = new SmtpClient(NotifyServer, NotifyPort)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(NotifyUser, NotifyPassword)
        };

        await client.SendMailAsync(mailMessage);
    }
}
