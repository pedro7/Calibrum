namespace Calibrum.Connection;

public class RemoteHttpConnection : HttpConnection
{
    public RemoteHttpConnection(Uri uri, Dictionary<string, string> headers)
    {
        httpClient.BaseAddress = uri;
        foreach (var header in headers)
        {
            httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
    }

    public void ChangeHeaders(Dictionary<string, string> headers)
    {
        foreach (var header in headers)
        {
            httpClient.DefaultRequestHeaders.Remove(header.Key);
            httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
    }

    public void ChangeBaseAddress(Uri uri)
    {
        httpClient.BaseAddress = uri;
    }
}