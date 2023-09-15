
using Core.ApplicationState;
using Core.Collections;
using static UI.Window.Main.FileList;

namespace UI.Window.Main;

public class OpenConfigPopover : Gtk.Popover
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Box? content;
    [Gtk.Connect] private readonly Gtk.Label? noFilesLabel;
#pragma warning restore 0649

    private readonly FileList fileList;

    public event EventHandler? OnSelectConfigFile;
    public event FileHandler? OnFileSelected;

    private OpenConfigPopover(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        ObservableHashSet<string> files = StateManager.State.RecentFiles;
        fileList = new FileList(files);
        files.CollectionChanged += (_, _) =>
        {
            noFilesLabel!.SetVisible(files.Count == 0);
            fileList.UpdateRecentFiles(files);
        };
        fileList.OnFileSelected += (file) => OnFileSelected?.Invoke(file);
        fileList.OnFileDelete += (file) => files.Remove(file);

        content!.Prepend(fileList);
        noFilesLabel!.SetVisible(files.Count == 0);

    }

    public OpenConfigPopover() : this(new Gtk.Builder("OpenConfigPopover.ui"), "openConfigPopover")
    {

    }
}
