using core.Collections;
using MessagePack;

namespace core.ApplicationSettings;

[MessagePackObject]
public class ApplicationSettings
{
    [Key(0)]
    public ObservableHashSet<string> RecentFiles { get; set; } = new();
}