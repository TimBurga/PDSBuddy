using Coravel.Invocable;
using Microsoft.Extensions.Logging;

namespace PDSBuddy;

public class PdsBackupJob : IInvocable
{
    private readonly PdsClient _pdsClient;
    private readonly GithubService _github;
    private readonly ILogger _logger;

    public PdsBackupJob(PdsClient pdsClient, GithubService github, ILogger logger)
    {
        _pdsClient = pdsClient;
        _github = github;
        _logger = logger;
    }

    public async Task Invoke()
    {
        try
        {
            _logger.LogInformation("Running PDS backup job");
            var content = await _pdsClient.GetRepoBytes(Config.DID);

            await _github.SaveBackup(content);
            _logger.LogInformation("Completed PDS backup job");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during PDS backup job");
        }
    }
}
