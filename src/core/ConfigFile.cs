using System;

public class ConfigFile
{
    private string path;
    private IDictionary<ConfigKey, string> raw;

    public ConfigFile(string path)
    {
        this.path = path;
        this.raw = new Dictionary<ConfigKey, string>();
        foreach (var line in File.ReadLines(path))
        {
            if (!line.StartsWith('#') && line.Contains('='))
            {
                var data = line.Split('=');
                raw.Add(Enum.Parse<ConfigKey>(value: data[0].Trim(), ignoreCase: true), data[1].Trim());
            }
        }
    }

    public string Get(ConfigKey key)
    {
        return raw[key];
    }

    public void Set(ConfigKey key, string value)
    {
        raw[key] = value;
    }

    public void Save()
    {
        using StreamWriter writer = new(this.path);
        foreach (KeyValuePair<ConfigKey, string> entry in raw)
        {
            var key = entry.Key.ToString();
            writer.WriteLine(string.Format("{0} = {1}", char.ToLower(key[0]) + key[1..], entry.Value));
        }
    }
}