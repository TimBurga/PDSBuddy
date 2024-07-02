using System.Net.Http.Json;
using PDSBuddy.Models;

namespace PDSBuddy;

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

    public async Task<BlobList> GetBlobList(string did, string cursor)
    {
        var response = await _client.GetAsync($"xrpc/com.atproto.sync.listBlobs?did={did}&cursor={cursor}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<BlobList>();
    }
}
    