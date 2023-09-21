using System.Text.Json;

namespace Core.ApplicationState;

public static class StateManager
{
    private static readonly string Location = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + Path.DirectorySeparatorChar + APP_ID + Path.DirectorySeparatorChar + "state.json";

    private static State? instance;

    private static void Persist(State settings)
    {
        File.Delete(Location);
        byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(settings, SourceGenerationContext.Default.State);
        File.WriteAllBytes(Location, bytes);
    }

    public static State State
    {
        get
        {
            if (instance is null)
            {
                _ = Directory.CreateDirectory(Path.GetDirectoryName(Location)!);
                if (!File.Exists(Location))
                {
                    instance = new State();
                    Persist(instance);
                }
                else
                {
                    byte[] bytes = File.ReadAllBytes(Location);
                    instance = JsonSerializer.Deserialize(bytes, SourceGenerationContext.Default.State)!;
                }
            }

            return instance;
        }
    }

    public static void Persist()
    {
        if (instance is not null)
        {
            Persist(instance);
        }
    }
}