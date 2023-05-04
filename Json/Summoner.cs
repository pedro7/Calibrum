namespace Calibrum.Json;

public class Summoner
{
    public string Id { get; set; } = string.Empty;

    public string AccountId { get; set; } = string.Empty;

    public string Puuid { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int ProfileIconId { get; set; }

    public long RevisionDate { get; set; }

    public int SummonerLevel { get; set; }
}