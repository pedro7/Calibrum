namespace Calibrum.Connection;

public class LeagueClient
{
    private readonly LocalHttpConnection localHttpConnection;

    private LeagueClient(LocalHttpConnection localHttpConnection)
    {
        this.localHttpConnection = localHttpConnection;
    }

    public static async Task OpenNewClientAndLogin(string username, string password)
    {
        RiotClient.OpenNewClient("--allow-multiple-clients --launch-product=league_of_legends --launch-patchline=live");
        await Task.Delay(7500);
        RiotClient client = RiotClient.GetOpenedClients()[0];
        await client.Login(username, password);
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

    public async Task<HttpResponseMessage> GetRegionLocale()
    {
        return await localHttpConnection.CallEndpoint(HttpMethod.Get, "riotclient/get_region_locale");
    }

    public async Task<HttpResponseMessage> GetSession()
    {
        return await localHttpConnection.CallEndpoint(HttpMethod.Get, "lol-login/v1/session");
    }
}
