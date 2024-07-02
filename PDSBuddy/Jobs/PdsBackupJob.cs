using Coravel.Invocable;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PDSBuddy.Jobs;

public class PdsBackupJob : IInvocable
{
    private readonly PdsClient _pdsClient;
    private readonly GithubService _github;
    private readonly ILogger _logger;
    private readonly MailService _mail;

    private readonly string? Did;
    private readonly string? GithubRepo;

    public PdsBackupJob(PdsClient pdsClient, GithubService github, ILogger<PdsBackupJob> logger, MailService mail, IConfiguration config)
    {
        _pdsClient = pdsClient;
        _github = github;
        _logger = logger;
        _mail = mail;
        Did = config.GetValue<string>("DID");
        GithubRepo = config.GetValue<string>("GITHUB_REPO");
    }

    public async Task Invoke()
    {
        try
        {
            _logger.LogInformation("Running PDS backup job");
            var content = await _pdsClient.GetRepoBytes(Did);

            await _github.SaveBackup(content);
            _logger.LogInformation("Completed PDS backup job");
            await _mail.Send($"PDS backed up to {GithubRepo}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during PDS backup job");
            await _mail.SendError(ex.Message);
        }
    }
}
