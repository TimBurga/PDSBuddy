using Coravel.Invocable;

namespace PDSBuddy;

public class PdsBackupJob : IInvocable
{
    private readonly PdsClient _pdsClient;
    private readonly GithubService _github;

    public PdsBackupJob(PdsClient pdsClient, GithubService github)
    {
        _pdsClient = pdsClient;
        _github = github;
    }

    public async Task Invoke()
    {
        var content = await _pdsClient.GetRepoBytes(Config.DID);

        await _github.SaveBackup(content);
    }
}

public class Config
{
    public static Uri PDS_URL = new Uri(Environment.GetEnvironmentVariable("PDS_URL"));
    public static string DID = Environment.GetEnvironmentVariable("DID");
    public static string GITHUB_REPO = Environment.GetEnvironmentVariable("GITHUB_REPO") ?? "PDSBackup";
    public static string GITHUB_TOKEN = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
}