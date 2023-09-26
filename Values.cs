#pragma warning disable CA1707

using System.Text.Json;

namespace Constants;

public class Values
{
    private static Values? _instance;

    public static Values Instance()
    {
        if (_instance is null)
        {
            byte[] bytes = File.ReadAllBytes("./global.json");
            _instance = JsonSerializer.Deserialize<Values>(bytes)!;
        }

        return _instance;

    }

    private Values() { }

    public string? APP_ID { get; set; }
    public string? APP_VERSION { get; set; }
    public string? APP_SHORT_NAME { get; set; }
    public string? APP_DISPLAY_NAME { get; set; }
    public string? BLUEPRINT_FOLDER { get; set; }
    public string? PO_FOLDER { get; set; }
}