using Microsoft.Extensions.Hosting;

namespace PDSBuddy;

public class Config
{
    public static Uri PDS_URL { get; set; }
    public static string DID { get; set; }
    public static string GITHUB_REPO { get; set; }
    public static string GITHUB_TOKEN { get; set; }
    public static bool NOTIFICATIONS_ENABLED { get; set; }
    public static string NOTIFICATIONS_FROM_ADDRESS { get; internal set; }
    public static string NOTIFICATIONS_TO_ADDRESS { get; internal set; }
    public static string NOTIFICATIONS_SUBJECT { get; internal set; }
    public static string NOTIFICATIONS_SERVER { get; internal set; }
    public static int NOTIFICATIONS_SERVER_PORT { get; internal set; }
    public static string NOTIFICATIONS_SERVER_USER { get; internal set; }
    public static string NOTIFICATIONS_SERVER_PASSWORD { get; internal set; }
}

public static class HostExtensions
{
    public static void ImportConfig(this IHost app)
    {
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PDS_URL")))
        {
            throw new ArgumentNullException(nameof(Config.PDS_URL), "Environment variable PDS_URL not found");
        }

        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DID")))
        {
            throw new ArgumentNullException(nameof(Config.DID), "Environment variable DID not found");
        }

        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GITHUB_REPO")))
        {
            throw new ArgumentNullException(nameof(Config.GITHUB_REPO), "Environment variable GITHUB_REPO not found");
        }

        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GITHUB_TOKEN")))
        {
            throw new ArgumentNullException(nameof(Config.GITHUB_TOKEN), "Environment variable GITHUB_TOKEN not found");
        }

        Config.PDS_URL = new Uri(Environment.GetEnvironmentVariable("PDS_URL"));
        Config.DID = Environment.GetEnvironmentVariable("DID");
        Config.GITHUB_REPO = Environment.GetEnvironmentVariable("GITHUB_REPO");
        Config.GITHUB_TOKEN = Environment.GetEnvironmentVariable("GITHUB_TOKEN");

        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NOTIFICATIONS_ENABLED")))
        {
            throw new ArgumentNullException(nameof(Config.NOTIFICATIONS_ENABLED), "Environment variable NOTIFICATIONS_ENABLED not found");
        }

        if (bool.TryParse(Environment.GetEnvironmentVariable("NOTIFICATIONS_ENABLED"), out var notifsEnabled))
        {
            Config.NOTIFICATIONS_ENABLED = notifsEnabled;
            if (!notifsEnabled)
            {
                return;
            }

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NOTIFICATIONS_FROM_ADDRESS")))
            {
                throw new ArgumentNullException(nameof(Config.NOTIFICATIONS_FROM_ADDRESS), "Environment variable NOTIFICATIONS_FROM_ADDRESS not found");
            }

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NOTIFICATIONS_TO_ADDRESS")))
            {
                throw new ArgumentNullException(nameof(Config.NOTIFICATIONS_TO_ADDRESS), "Environment variable NOTIFICATIONS_TO_ADDRESS not found");
            }

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NOTIFICATIONS_SUBJECT")))
            {
                throw new ArgumentNullException(nameof(Config.NOTIFICATIONS_SUBJECT), "Environment variable NOTIFICATIONS_SUBJECT not found");
            }

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NOTIFICATIONS_SERVER")))
            {
                throw new ArgumentNullException(nameof(Config.NOTIFICATIONS_SERVER), "Environment variable NOTIFICATIONS_SERVER not found");
            }

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NOTIFICATIONS_SERVER_PORT")))
            {
                throw new ArgumentNullException(nameof(Config.NOTIFICATIONS_SERVER_PORT), "Environment variable NOTIFICATIONS_SERVER_PORT not found");
            }

            if (!int.TryParse(Environment.GetEnvironmentVariable("NOTIFICATIONS_SERVER_PORT"), out _))
            {
                throw new ArgumentNullException(nameof(Config.NOTIFICATIONS_SERVER_PORT), "Environment variable NOTIFICATIONS_SERVER_PORT must be numeric");
            }

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NOTIFICATIONS_SERVER_USER")))
            {
                throw new ArgumentNullException(nameof(Config.NOTIFICATIONS_SERVER_USER), "Environment variable NOTIFICATIONS_SERVER_USER not found");
            }

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NOTIFICATIONS_SERVER_PASSWORD")))
            {
                throw new ArgumentNullException(nameof(Config.NOTIFICATIONS_SERVER_PASSWORD), "Environment variable NOTIFICATIONS_SERVER_PASSWORD not found");
            }

            Config.NOTIFICATIONS_FROM_ADDRESS = Environment.GetEnvironmentVariable("NOTIFICATIONS_FROM_ADDRESS");
            Config.NOTIFICATIONS_TO_ADDRESS = Environment.GetEnvironmentVariable("NOTIFICATIONS_TO_ADDRESS");
            Config.NOTIFICATIONS_SUBJECT = Environment.GetEnvironmentVariable("NOTIFICATIONS_SUBJECT");
            Config.NOTIFICATIONS_SERVER = Environment.GetEnvironmentVariable("NOTIFICATIONS_SERVER");
            Config.NOTIFICATIONS_SERVER_PORT = int.Parse(Environment.GetEnvironmentVariable("NOTIFICATIONS_SERVER_PORT"));
            Config.NOTIFICATIONS_SERVER_USER = Environment.GetEnvironmentVariable("NOTIFICATIONS_SERVER_USER");
            Config.NOTIFICATIONS_SERVER_PASSWORD = Environment.GetEnvironmentVariable("NOTIFICATIONS_SERVER_PASSWORD");
        }
    }
}