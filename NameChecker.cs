using Calibrum.Connection;
using Calibrum.Enum;
using Calibrum.Json;
using System.Net.Http.Json;

namespace Calibrum.NameChecker;

public static class NameChecker
{
    public async static Task<DateTimeOffset> GetNameAvailabilityDatetime(Server server, string key, string summonerName)
    {
        LeagueApi leagueApi = new(server, key);
        Summoner summoner = (await leagueApi.CallEndpoint(HttpMethod.Get, $"lol/summoner/v4/summoners/by-name/{summonerName}").Result.Content.ReadFromJsonAsync<Summoner>())!;
        return GetCleanupDate(summoner.RevisionDate, summoner.SummonerLevel).ToLocalTime();
    }

    private static DateTimeOffset GetCleanupDate(long revisionDate, int summonerLevel)
    {
        if (summonerLevel >= 30)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(revisionDate).AddMonths(30);
        }
        else if (summonerLevel <= 6)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(revisionDate).AddMonths(6);
        }
        else
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(revisionDate).AddMonths(summonerLevel);
        }
    }
}