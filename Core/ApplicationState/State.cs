using Core.Collections;
using MessagePack;

namespace Core.ApplicationState;

[MessagePackObject]
public class State
{
    [Key(0)]
    public ObservableHashSet<string> RecentFiles { get; set; } = new();
}