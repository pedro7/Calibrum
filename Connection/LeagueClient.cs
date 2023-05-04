using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Text;

namespace Calibrum.Connection;

public class LeagueClient : HttpConnection
{
    private LeagueClient(string port, string auth)
    {
        httpClient.BaseAddress = new Uri($"https://127.0.0.1:{port}/");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
    }

    public static LeagueClient[] GetOpenedClients()
    {
        string output = GetProcessOutput();
        string[] ports = GetPorts(output);
        string[] auths = GetAuths(GetPasswords(output));
        LeagueClient[] clients = new LeagueClient[ports.Length];
        for (int i = 0; i < clients.Length; i++)
        {
            clients[i] = new LeagueClient(ports[i], auths[i]);
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
                Arguments = "/c wmic PROCESS WHERE name='LeagueClientUx.exe' GET commandline",
                RedirectStandardOutput = true
            }
        };
        cmd.Start();
        return cmd.StandardOutput.ReadToEnd();
    }

    private static string[] GetPorts(string output)
    {
        MatchCollection matchCollection = Regex.Matches(output, @"--app-port=([^""]*)");
        string[] ports = new string[matchCollection.Count];
        for (int i = 0; i < ports.Length; i++)
        {
            ports[i] = matchCollection[i].Groups[1].Value;
        }
        return ports;
    }

    private static string[] GetPasswords(string output)
    {
        MatchCollection matchCollection = Regex.Matches(output, @"--remoting-auth-token=([^""]*)");
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
