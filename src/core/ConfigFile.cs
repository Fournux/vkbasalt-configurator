using System.Globalization;

namespace core;

public class ConfigFile
{
    public string Path { get; set; }
    private IDictionary<ConfigKey, string> raw;

    public ConfigFile(string path)
    {
        Path = path;
        raw = new Dictionary<ConfigKey, string>();
        foreach (var line in File.ReadLines(Path))
        {
            if (!line.StartsWith('#') && line.Contains('='))
            {
                var data = line.Split('=');
                raw.Add(Enum.Parse<ConfigKey>(value: data[0].Trim(), ignoreCase: true), data[1].Trim());
            }
        }
    }

    public T Get<T>(ConfigKey key)
    {
        string value = raw.ContainsKey(key) ? raw[key] : DefaultValue(key);
        return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
    }

    public void Set<T>(ConfigKey key, T _value)
    {
        string str;
        if (_value is double value) str = value.ToString("0.#####", CultureInfo.InvariantCulture)!;
        else str = _value!.ToString()!;

        raw[key] = str;
    }

    public void Save()
    {
        using StreamWriter writer = new(Path);
        foreach (KeyValuePair<ConfigKey, string> entry in raw)
        {
            var key = entry.Key.ToString();
            writer.WriteLine(string.Format("{0} = {1}", char.ToLower(key[0]) + key[1..], entry.Value));
        }
    }

    private static string DefaultValue(ConfigKey key)
    {
        return key switch
        {
            ConfigKey.CasSharpness => "0.4",
            ConfigKey.DepthCapture => "off",
            ConfigKey.DlsDenoise => "0.17",
            ConfigKey.DlsSharpness => "0.5",
            ConfigKey.Effects => "cas",
            ConfigKey.EnableOnLaunch => "True",
            ConfigKey.FxaaQualityEdgeThreshold => "0.125",
            ConfigKey.FxaaQualityEdgeThresholdMin => "0.0312",
            ConfigKey.FxaaQualitySubpix => "0.75",
            ConfigKey.LutFile => "",
            ConfigKey.ReshadeIncludePath => "",
            ConfigKey.ReshadeTexturePath => "",
            ConfigKey.SmaaCornerRounding => "25",
            ConfigKey.SmaaEdgeDetection => "luma",
            ConfigKey.SmaaMaxSearchSteps => "32",
            ConfigKey.SmaaMaxSearchStepsDiag => "16",
            ConfigKey.SmaaThreshold => "0.05",
            ConfigKey.ToggleKey => "Home",
            _ => ""
        };
    }
}