using System.Net.Http.Headers;
using System.Net.Http.Json;
using Calibrum.Json;

namespace Calibrum.Connection;

public class LeagueStore : HttpConnection
{
    public LeagueStore(LeagueClient leagueClient)
    {
        Connect(leagueClient);
    }

    private async void Connect(LeagueClient leagueClient)
    {
        string platform = GetPlatform(await GetServer(leagueClient));
        string idToken = await GetIdToken(leagueClient);
        httpClient.BaseAddress = new Uri($"https://{platform}.store.leagueoflegends.com/storefront/v3/");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", idToken);
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Other");
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
        return (await leagueClient.CallEndpoint(HttpMethod.Get, "riotclient/get_region_locale").Result.Content.ReadFromJsonAsync<RegionLocale>())!.Region;
    }

    private static async Task<string> GetIdToken(LeagueClient leagueClient)
    {
        return (await leagueClient.CallEndpoint(HttpMethod.Get, "lol-login/v1/session").Result.Content.ReadFromJsonAsync<Session>())!.IdToken;
    }
}
