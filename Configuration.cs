#pragma warning disable CA1050

using System.Text.Json;

public static class Configuration
{
    public const string Path = "config.json";
    private static readonly IDictionary<string, string> config;

    static Configuration()
    {
        string text = File.ReadAllText(Path);
        config = JsonSerializer.Deserialize<Dictionary<string, string>>(text)!;
    }

    public static string Get(string key)
    {
        return config![key];
    }
}