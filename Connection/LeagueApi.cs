using Calibrum.Enum;

namespace Calibrum.Connection;

public class LeagueApi : RiotApi
{
    public LeagueApi(Server server, string key) : base(key, GetPlatform(server)) { }

    public async Task<HttpResponseMessage> GetSummoner(string summonerName)
    {
        return await remoteHttpConnection.CallEndpoint(HttpMethod.Get, $"lol/summoner/v4/summoners/by-name/{summonerName}");
    }

    private static string GetPlatform(Server server)
    {
        return server switch
        {
            Server.BR => "br1",
            Server.EUNE => "eun1",
            Server.EUW => "euw1",
            Server.JP => "jp1",
            Server.KR => "kr",
            Server.LAN => "la1",
            Server.LAS => "la2",
            Server.NA => "na1",
            Server.OCE => "oc1",
            Server.PH => "ph2",
            Server.RU => "ru",
            Server.SG => "sg2",
            Server.TH => "th2",
            Server.TR => "tr1",
            Server.TW => "tw2",
            Server.VN => "vn2",
            _ => throw new ArgumentException("Invalid server.")
        };
    }
}
