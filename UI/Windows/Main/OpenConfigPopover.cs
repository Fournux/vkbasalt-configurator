
using Core.ApplicationState;
using Core.Collections;
using static UI.Windows.Main.FileList;

namespace UI.Windows.Main;

public class OpenConfigPopover : Gtk.Popover
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Box? content;
    [Gtk.Connect] private readonly Gtk.Box? noRecentFilesIndicator;
#pragma warning restore 0649

    private readonly FileList fileList;

    public event FileHandler? OnFileSelected;
    public event FileHandler? OnFileDelete;


    private OpenConfigPopover(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        ObservableHashSet<string> files = StateManager.State.RecentFiles;
        fileList = new FileList(files);
        fileList.SetMarginBottom(10);
        fileList.SetMarginEnd(10);
        fileList.SetMarginStart(10);
        files.CollectionChanged += (_, _) =>
        {
            fileList.UpdateRecentFiles(files);
            noRecentFilesIndicator!.SetVisible(files.Count == 0);
        };
        fileList.OnFileSelected += (file) => OnFileSelected?.Invoke(file);
        fileList.OnFileDelete += (file) =>
        {
            _ = files.Remove(file);
            OnFileDelete?.Invoke(file);
        };
        noRecentFilesIndicator!.SetVisible(files.Count == 0);
        content!.Append(fileList);
    }

    public OpenConfigPopover() : this(new Gtk.Builder("OpenConfigPopover.ui"), "openConfigPopover")
    {

    }
}
