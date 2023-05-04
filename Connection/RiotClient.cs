using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Text;

namespace Calibrum.Connection;

public class RiotClient : HttpConnection
{
    private RiotClient(string port, string auth)
    {
        httpClient.BaseAddress = new Uri($"https://127.0.0.1:{port}/");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
    }

    public async Task<HttpResponseMessage> Login(string username, string password)
    {
        return await CallEndpoint(HttpMethod.Put, "rso-auth/v1/session/credentials", $@"{{""username"":""{username}"",""password"":""{password}""}}");
    }

    public static void OpenNewClient(string arguments)
    {
        using Process riotClient = new()
        {
            StartInfo = new()
            {
                FileName = @"C:\Riot Games\Riot Client\RiotClientServices.exe",
                Arguments = arguments
            }
        };
        riotClient.Start();
    }

    public static RiotClient[] GetOpenedClients()
    {
        string output = GetProcessOutput();
        string[] ports = GetPorts(output);
        string[] auths = GetAuths(GetPasswords(output));
        RiotClient[] clients = new RiotClient[ports.Length];
        for (int i = 0; i < ports.Length; i++)
        {
            clients[i] = new RiotClient(ports[i], auths[i]);
        }
        return clients;
    }

    private static string GetProcessOutput()
    {
        using Process cmd = new()
        {
            StartInfo = new()
            {
                FileName = "cmd.exe",
                Arguments = "/c wmic PROCESS WHERE name='RiotClientUx.exe' GET commandline",
                RedirectStandardOutput = true
            }
        };
        cmd.Start();
        return cmd.StandardOutput.ReadToEnd();
    }

    private static string[] GetPorts(string output)
    {
        MatchCollection matchCollection = Regex.Matches(output, @"--app-port=([^ ]*)");
        string[] ports = new string[matchCollection.Count];
        for (int i = 0; i < ports.Length; i++)
        {
            ports[i] = matchCollection[i].Groups[1].Value;
        }
        return ports;
    }

    private static string[] GetPasswords(string output)
    {
        MatchCollection matchCollection = Regex.Matches(output, @"--remoting-auth-token=([^ ]*)");
        string[] passwords = new string[matchCollection.Count];
        for (int i = 0; i < passwords.Length; i++)
        {
            passwords[i] = matchCollection[i].Groups[1].Value;
        }
        return passwords;
    }

    private static string[] GetAuths(string[] passwords)
    {
        string[] auths = new string[passwords.Length];
        for (int i = 0; i < auths.Length; i++)
        {
            byte[] encoded = Encoding.ASCII.GetBytes($"riot:{passwords[i]}");
            auths[i] = Convert.ToBase64String(encoded);
        }
        return auths;
    }
}
