using Core.ApplicationState;
using Core.Collections;
using UI.Components;
using UI.Helper;

namespace UI.MainWindow.HomeView;

public class View : Gtk.Box
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Button? buttonExisting;
    [Gtk.Connect] private readonly Gtk.Button? buttonNew;
    [Gtk.Connect] private readonly Gtk.ListBox? recentFilesContainer;
#pragma warning restore 0649
    private readonly Gtk.Window? window;
    private readonly ICollection<DeleteRow> recentFileRows = new List<DeleteRow>();
    public delegate void FileSelectedCallback(string file);
    public event FileSelectedCallback? OnFileSelected;

    private View(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        buttonExisting!.OnClicked += async (sender, args) =>
        {
            Gio.File? file = await GtkHelper.Select(window!, "Select a file", "Open");
            if (file != null)
            {
                OnFileSelected?.Invoke(file.GetPath()!);
            }
        };

        buttonNew!.OnClicked += async (sender, args) =>
        {
            Gio.File? file = await GtkHelper.SelectFolder(window!, "Select a folder", "Open");

        };

        ObservableHashSet<string> files = StateManager.State.RecentFiles;

        files.CollectionChanged += (_, _) => UpdateRecentFiles(files);

        UpdateRecentFiles(files);
    }

    public View(Gtk.Window window) : this(new Gtk.Builder("HomeView.ui"), "homeView")
    {
        this.window = window;
    }

    public void UpdateRecentFiles(ICollection<string> files)
    {
        foreach (DeleteRow file in recentFileRows)
        {
            recentFilesContainer!.Remove(file);
        }

        recentFileRows.Clear();

        if (files.Count == 0)
        {
            recentFilesContainer!.SetVisible(false);
        }
        else
        {
            recentFilesContainer!.SetVisible(true);
        }

        recentFilesContainer.OnRowActivated += (sender, args) =>
        {
            DeleteRow row = (DeleteRow)args.Row;
            OnFileSelected?.Invoke(row.GetTitle());
        };

        recentFilesContainer.SetActivateOnSingleClick(true);

        foreach (string file in files)
        {
            DeleteRow row = new(file);
            row.OnDelete += () => StateManager.State.RecentFiles.Remove(file);
            row.SetActivatable(true);
            recentFileRows!.Add(row);
            recentFilesContainer!.Prepend(row);
        }
    }
}
