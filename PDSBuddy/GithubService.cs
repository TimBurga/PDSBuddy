using Octokit;

namespace PDSBuddy;

public class GithubService
{
    private readonly GitHubClient _client;

    private const string BackupFilename = "backup.car";
    private const string CommitMessage = $"PDS Backup";
    private User? User;

    public GithubService()
    {
        _client = new GitHubClient(new ProductHeaderValue("PDSBuddy"))
        {
            Credentials = new Credentials(Config.GITHUB_TOKEN)
        };
    }

    public async Task SaveBackup(byte[] backupFile)
    {
        try
        {
            var existing = await _client.Repository.Content.GetAllContents(await GetUserName(), Config.GITHUB_REPO, BackupFilename);

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
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    private async Task CreateBackup(string encoded)
    {
        var request = new CreateFileRequest($"{CommitMessage} {DateTime.Today.ToShortDateString()}", encoded, true);
        await _client.Repository.Content.CreateFile(await GetUserName(), Config.GITHUB_REPO, BackupFilename, request);
    }

    private async Task UpdateBackup(string encoded, string sha)
    {
        var request = new UpdateFileRequest($"{CommitMessage} {DateTime.Today.ToShortDateString()}", encoded, sha);
        await _client.Repository.Content.UpdateFile(await GetUserName(), Config.GITHUB_REPO, BackupFilename, request);
    }

    private async Task<string> GetUserName()
    {
        User ??= await _client.User.Current();
        return User.Login;
    }
}
