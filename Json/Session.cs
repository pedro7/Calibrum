namespace Calibrum.Json
{
    public class Session
    {
        public int AccountId { get; set; }
        public bool Connected { get; set; }
        public string IdToken { get; set; } = string.Empty;
        public bool IsInLoginQueue { get; set; }
        public bool IsNewPlayer { get; set; }
        public string Puuid { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int SummonerId { get; set; }
        public string UserAuthToken { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
