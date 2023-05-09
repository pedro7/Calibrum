using Calibrum.Json;
using System.Net.Http.Json;

namespace Calibrum.Connection;

public class LeagueStore
{
    private readonly RemoteHttpConnection remoteHttpConnection;
    
    private LeagueStore(RemoteHttpConnection remoteHttpConnection)
    {
        this.remoteHttpConnection = remoteHttpConnection;
    }

    public static async Task<LeagueStore> GetLeagueStore(LeagueClient leagueClient)
    {
        string platform = GetPlatform(await GetServer(leagueClient));
        string idToken = await GetIdToken(leagueClient);
        Dictionary<string, string> headers = new()
        {
            { "Authorization", $"Bearer {idToken}" },
            { "User-Agent", "Other" }
        };
        return new(new(new Uri($"https://{platform}.store.leagueoflegends.com/storefront/v3/"), headers));
    }

    public async Task<HttpResponseMessage> GetPurchaseHistory()
    {
        return await remoteHttpConnection.CallEndpoint(HttpMethod.Get, "history/purchase");
    }

    private static string GetPlatform(string server)
    {
        return server switch
        {
            "BR" => "br",
            "EUNE" => "eun",
            "EUW" => "euw",
            "JP" => "jp",
            "KR" => "kr",
            "LAN" => "la1",
            "LAS" => "la2",
            "NA" => "na",
            "OCE" => "oc",
            "PH" => "ph2",
            "RU" => "ru",
            "SG" => "sg2",
            "TH" => "th2",
            "TR" => "tr",
            "TW" => "tw2",
            "VN" => "vn2",
            "PBE" => "pbe",
            _ => throw new ArgumentException("Invalid server.")
        };
    }

    private static async Task<string> GetServer(LeagueClient leagueClient)
    {
        return (await leagueClient.GetRegionLocale().Result.Content.ReadFromJsonAsync<RegionLocale>())!.Region;
    }

    private static async Task<string> GetIdToken(LeagueClient leagueClient)
    {
        return (await leagueClient.GetSession().Result.Content.ReadFromJsonAsync<Session>())!.IdToken;
    }
}
