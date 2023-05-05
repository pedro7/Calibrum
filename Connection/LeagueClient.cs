using Calibrum.Json;
using System.Net.Http.Json;

namespace Calibrum.Connection;

public class LeagueClient
{
    private readonly LocalHttpConnection localHttpConnection;

    public LeagueClient(LocalHttpConnection localHttpConnection)
    {
        this.localHttpConnection = localHttpConnection;
    }

    public static LeagueClient[] GetOpenedClients()
    {
        var localHttpConnections = LocalHttpConnection.GetOpenedConnections("LeagueClientUx.exe");
        var leagueClients = new LeagueClient[localHttpConnections.Length];
        for (int i = 0; i < localHttpConnections.Length; i++)
        {
            leagueClients[i] = new(localHttpConnections[i]);
        }
        return leagueClients;
    }

    public async Task<HttpResponseMessage> AcceptMatch()
    {
        return await localHttpConnection.CallEndpoint(HttpMethod.Post, "lol-matchmaking/v1/ready-check/accept");
    }

    public async Task<string> GetServer()
    {
        return (await localHttpConnection.CallEndpoint(HttpMethod.Get, "riotclient/get_region_locale").Result.Content.ReadFromJsonAsync<RegionLocale>())!.Region;
    }

    public async Task<string> GetIdToken()
    {
        return (await localHttpConnection.CallEndpoint(HttpMethod.Get, "lol-login/v1/session").Result.Content.ReadFromJsonAsync<Session>())!.IdToken;
    }
}
