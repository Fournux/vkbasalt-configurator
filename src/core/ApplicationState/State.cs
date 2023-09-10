using core.Collections;
using MessagePack;

namespace core.ApplicationState;

[MessagePackObject]
public class State
{
    [Key(0)]
    public ObservableHashSet<string> RecentFiles { get; set; } = new();
}