using Microsoft.Extensions.Configuration;
using Octokit;

namespace PDSBuddy;

public class GithubService
{
    private readonly GitHubClient _client;
    private readonly string GithubRepo;

    private const string BackupFilename = "backup.car";
    private const string CommitMessage = $"PDS Backup";
    private User? User;

    public GithubService(IConfiguration config)
    {
        GithubRepo = config.GetValue<string>("GITHUB_REPO");

        _client = new GitHubClient(new ProductHeaderValue("PDSBuddy"))
        {
            Credentials = new Credentials(config.GetValue<string>("GITHUB_TOKEN"))
        };
    }

    public async Task SaveBackup(byte[] backupFile)
    {
        var existing = await _client.Repository.Content.GetAllContents(await GetUserName(), GithubRepo, BackupFilename);

        var encoded = Convert.ToBase64String(backupFile);
        if (!existing.Any())
        {
            await CreateBackup(encoded);
        }
        else
        {
            await UpdateBackup(encoded, existing.First().Sha);
        }
    }

    private async Task CreateBackup(string encoded)
    {
        var request = new CreateFileRequest($"{CommitMessage} {DateTime.Today.ToShortDateString()}", encoded, false);
        await _client.Repository.Content.CreateFile(await GetUserName(), GithubRepo, BackupFilename, request);
    }

    private async Task UpdateBackup(string encoded, string sha)
    {
        var request = new UpdateFileRequest($"{CommitMessage} {DateTime.Today.ToShortDateString()}", encoded, sha);
        await _client.Repository.Content.UpdateFile(await GetUserName(), GithubRepo, BackupFilename, request);
    }

    private async Task<string> GetUserName()
    {
        User ??= await _client.User.Current();
        return User.Login;
    }
}
