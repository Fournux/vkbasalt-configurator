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
    [Gtk.Connect] private readonly Gtk.Box? recentFiles;
#pragma warning restore 0649
    private readonly Gtk.Window? window;
    private readonly FileList fileList;
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
        fileList = new FileList(files);
        files.CollectionChanged += (_, _) => fileList.UpdateRecentFiles(files);
        fileList.OnFileSelected += (file) => OnFileSelected?.Invoke(file);
        recentFiles!.Append(fileList);
    }

    public View(Gtk.Window window) : this(new Gtk.Builder("HomeView.ui"), "homeView")
    {
        this.window = window;
    }

}
