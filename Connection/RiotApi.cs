namespace Calibrum.Connection;

public abstract class RiotApi
{
    protected readonly RemoteHttpConnection remoteHttpConnection;

    protected RiotApi(string key, string platform)
    {
        Dictionary<string, string> headers = new()
        {
            { "X-Riot-Token", key }
        };
        remoteHttpConnection = new(new($"https://{platform}.api.riotgames.com/"), headers);
    }

    protected void SetKey(string key)
    {
        Dictionary<string, string> headers = new()
        {
            { "X-Riot-Token", key }
        };
        remoteHttpConnection.ChangeHeaders(headers);
    }

    protected void SetPlatform(string platform)
    {
        remoteHttpConnection.ChangeBaseAddress(new($"https://{platform}.api.riotgames.com/"));
    }
}
