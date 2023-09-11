using MessagePack;

namespace core.ApplicationState;

public static class StateManager
{
    private static readonly string Location = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + Path.DirectorySeparatorChar + APP_ID + Path.DirectorySeparatorChar + "settings.msgpack";

    private static State? instance;

    private static void Persist(State settings)
    {
        File.Delete(Location);
        byte[] bytes = MessagePackSerializer.Serialize(settings);
        File.WriteAllBytes(Location, bytes);
    }

    public static State State
    {
        get
        {
            if (instance == null)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Location)!);
                if (!File.Exists(Location))
                {
                    instance = new State();
                    Persist(instance);
                }
                else
                {
                    byte[] bytes = File.ReadAllBytes(Location);
                    instance = MessagePackSerializer.Deserialize<State>(bytes);
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