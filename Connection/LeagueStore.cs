namespace Calibrum.Connection;

public class LeagueStore
{
    private readonly RemoteHttpConnection remoteHttpConnection;

    public LeagueStore(LeagueClient leagueClient)
    {
        string platform = GetPlatform(leagueClient.GetServer().Result);
        string idToken = leagueClient.GetIdToken().Result;
        Dictionary<string, string> headers = new()
        {
            { "Authorization", $"Bearer {idToken}" },
            { "User-Agent", "Other" }
        };
        remoteHttpConnection = new(new Uri($"https://{platform}.store.leagueoflegends.com/storefront/v3/"), headers);
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
}
