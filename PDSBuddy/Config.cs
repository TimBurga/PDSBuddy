namespace PDSBuddy;

public class Config
{
    public static Uri PDS_URL = new Uri(Environment.GetEnvironmentVariable("PDS_URL"));
    public static string DID = Environment.GetEnvironmentVariable("DID");
    public static string GITHUB_REPO = Environment.GetEnvironmentVariable("GITHUB_REPO");
    public static string GITHUB_TOKEN = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
    public static bool NOTIFICATIONS_ENABLED = bool.Parse(Environment.GetEnvironmentVariable("NOTIFICATIONS_ENABLED"));
    public static string NOTIFICATIONS_FROM_ADDRESS = Environment.GetEnvironmentVariable("NOTIFICATIONS_FROM_ADDRESS");
    public static string NOTIFICATIONS_TO_ADDRESS = Environment.GetEnvironmentVariable("NOTIFICATIONS_TO_ADDRESS");
    public static string NOTIFICATIONS_SUBJECT = Environment.GetEnvironmentVariable("NOTIFICATIONS_SUBJECT");
    public static string NOTIFICATIONS_SERVER = Environment.GetEnvironmentVariable("NOTIFICATIONS_SERVER");
    public static int NOTIFICATIONS_SERVER_PORT = int.Parse(Environment.GetEnvironmentVariable("NOTIFICATIONS_SERVER_PORT"));
    public static string NOTIFICATIONS_SERVER_USER = Environment.GetEnvironmentVariable("NOTIFICATIONS_SERVER_USER");
    public static string NOTIFICATIONS_SERVER_PASSWORD = Environment.GetEnvironmentVariable("NOTIFICATIONS_SERVER_PASSWORD");
}
