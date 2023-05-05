using System.Diagnostics;

namespace Calibrum.Connection;

public class RiotClient
{
    private readonly LocalHttpConnection localHttpConnection;

    public RiotClient(LocalHttpConnection localHttpConnection)
    {
        this.localHttpConnection = localHttpConnection;
    }

    public static void OpenNewClient(string arguments)
    {
        using Process riotClient = new()
        {
            StartInfo = new()
            {
                FileName = @"C:\Riot Games\Riot Client\RiotClientServices.exe",
                Arguments = arguments
            }
        };
        riotClient.Start();
    }

    public static RiotClient[] GetOpenedClients()
    {
        var localHttpConnections = LocalHttpConnection.GetOpenedConnections("RiotClientServices.exe");
        var riotClients = new RiotClient[localHttpConnections.Length];
        for (int i = 0; i < localHttpConnections.Length; i++)
        {
            riotClients[i] = new(localHttpConnections[i]);
        }
        return riotClients;
    }

    public async Task<HttpResponseMessage> Login(string username, string password)
    {
        return await localHttpConnection.CallEndpoint(HttpMethod.Put, "rso-auth/v1/session/credentials", $@"{{""username"":""{username}"",""password"":""{password}""}}");
    }
}
