using System.Text;

namespace Calibrum.Connection;

public abstract class HttpConnection
{
    protected readonly HttpClient httpClient = new(new HttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });

    public async Task<HttpResponseMessage> CallEndpoint(HttpMethod method, string endpoint)
    {
        return await httpClient.SendAsync(new(method, endpoint));
    }

    public async Task<HttpResponseMessage> CallEndpoint(HttpMethod method, string endpoint, string content)
    {
        HttpRequestMessage request = new(method, endpoint)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };
        return await httpClient.SendAsync(request);
    }
}
