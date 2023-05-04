using Calibrum.Connection;
using Calibrum.Enum;
using Calibrum.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Text.Json;
using Calibrum.NameChecker;

public static class Tester
{
    static async Task Main()
    {
        //LeagueClient leagueClient = LeagueClient.GetOpenedClients()[0];

        //Console.WriteLine(leagueClient.CallEndpoint(HttpMethod.Post, "lol-matchmaking/v1/ready-check/accept").Result.Content.ReadAsStringAsync().Result);

        //LeagueStore leagueStore = new(leagueClient);

        //Console.WriteLine(leagueStore.CallEndpoint(HttpMethod.Get, "history/purchase").Result.Content.ReadAsStringAsync().Result);

        //Console.WriteLine(NameChecker.GetNameAvailabilityDatetime(Server.BR, "RGAPI-cabbf65c-ea68-4882-ae38-6f32408ecd3a", "nightcore").Result);

        //RiotClient.OpenNewClient("--allow-multiple-clients --launch-product=league_of_legends --launch-patchline=live");
        //Thread.Sleep(5000);
        //RiotClient client = RiotClient.GetOpenedClients()[0];
        //await client.Login("pedrogabriieel1", "wait.Isw0retoher.Iswore");
    }
}