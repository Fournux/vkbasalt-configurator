using System.Collections.ObjectModel;
using Adw;
using MessagePack;

namespace core.ApplicationSettings;

public static class ApplicationSettingsManager
{
    private static readonly string Location = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + Path.DirectorySeparatorChar + APP_ID + Path.DirectorySeparatorChar + "settings.msgpack";

    private static ApplicationSettings? instance;

    private static void Persist(ApplicationSettings settings)
    {
        byte[] bytes = MessagePackSerializer.Serialize(settings);
        File.WriteAllBytes(Location, bytes);
    }

    public static ApplicationSettings Settings
    {
        get
        {
            if (instance == null)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Location)!);
                if (!File.Exists(Location))
                {
                    instance = new ApplicationSettings();
                    Persist(instance);
                }
                else
                {
                    byte[] bytes = File.ReadAllBytes(Location);
                    instance = MessagePackSerializer.Deserialize<ApplicationSettings>(bytes);
                }
            }

            return instance;
        }
    }

    public static void Persist()
    {
        if (instance != null)
        {
            Persist(instance);
        }
    }
}