namespace Calibrum.Connection;

public abstract class RiotApi : HttpConnection
{
    private string key = string.Empty;

    public string Key
    {
        get
        {
            return key;
        }
        set
        {
            if (!string.IsNullOrEmpty(key))
            {
                httpClient.DefaultRequestHeaders.Remove("X-Riot-Token");
            }
            httpClient.DefaultRequestHeaders.Add("X-Riot-Token", value);
            key = value;
        }
    }

    protected RiotApi(string key)
    {
        Key = key;
    }
}
