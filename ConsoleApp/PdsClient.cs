namespace ConsoleApp;

public class PdsClient
{
    private readonly HttpClient _client;

    public PdsClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<byte[]> GetRepoBytes(string did)
    {
        var response = await _client.GetAsync($"xrpc/com.atproto.sync.getRepo?did={did}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsByteArrayAsync();
    }
}
