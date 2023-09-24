using System.Text.Json.Serialization;
using Core.Collections;

namespace Core.ApplicationState;

public class State
{
    public ObservableHashSet<string> RecentFiles { get; set; } = new();

}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(State))]
internal sealed partial class SourceGenerationContext : JsonSerializerContext
{ }