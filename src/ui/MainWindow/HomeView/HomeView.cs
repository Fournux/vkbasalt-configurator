using core.ApplicationState;
using GLib;
using ui.Helper;

namespace ui;

public class HomeView : Gtk.Box
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Button? buttonExisting;
    [Gtk.Connect] private readonly Gtk.Button? buttonNew;
    [Gtk.Connect] private readonly Gtk.ListBox? recentFilesContainer;
#pragma warning restore 0649
    private readonly Gtk.Window? window;
    private ICollection<DeleteRow> recentFileRows = new List<DeleteRow>();
    public delegate void FileSelectedCallback(string file);
    public event FileSelectedCallback? OnFileSelected;

    private HomeView(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);
        buttonExisting!.OnClicked += async (sender, args) =>
        {
            Gio.File? file = await FileHelper.Select(window!, "Select a file", "Open");
            if (file != null)
            {
                OnFileSelected?.Invoke(file.GetPath()!);
            }
        };

        var files = StateManager.State.RecentFiles;

        files.CollectionChanged += (_, _) =>
        {
            UpdateRecentFiles(files);
        };

        UpdateRecentFiles(files);
    }

    public HomeView(Gtk.Window window) : this(new Gtk.Builder("HomeView.ui"), "homeView")
    {
        this.window = window;
    }

    public void UpdateRecentFiles(ICollection<string> files)
    {
        foreach (var file in recentFileRows)
        {
            recentFilesContainer!.Remove(file);
        }

        recentFileRows.Clear();

        if (files.Count == 0) recentFilesContainer!.SetVisible(false);
        else recentFilesContainer!.SetVisible(true);

        foreach (var file in files)
        {
            var row = new DeleteRow(file);

            row.OnNotify += (sender, args) =>
            {
                if (args.Pspec.GetName() == "has-focus" && row.GetHasFocus())
                {
                    OnFileSelected?.Invoke(file);
                }
            };
            recentFileRows!.Add(row);
            recentFilesContainer!.Prepend(row);
        }
    }
}
