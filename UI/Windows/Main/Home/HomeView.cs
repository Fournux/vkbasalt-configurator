using Core.ApplicationState;
using Core.Collections;
using UI.Helpers;
using static UI.Windows.Main.FileList;
using static UI.Localization.I18n;

namespace UI.Windows.Main.Home;

public class HomeView : Gtk.Box
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Button? buttonExisting;
    [Gtk.Connect] private readonly Gtk.Button? buttonNew;
    [Gtk.Connect] private readonly Gtk.Box? recentFiles;
#pragma warning restore 0649
    private readonly Gtk.Window? window;
    private readonly FileList fileList;
    public event EventHandler? OnSelectConfigFile;
    public event EventHandler? OnCreateConfigFile;
    public event FileHandler? OnFileSelected;

    private HomeView(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        buttonExisting!.OnClicked += (sender, args) => OnSelectConfigFile?.Invoke(sender, args);
        buttonNew!.OnClicked += (sender, args) => OnCreateConfigFile?.Invoke(sender, args);

        ObservableHashSet<string> files = StateManager.State.RecentFiles;
        fileList = new FileList(files);
        files.CollectionChanged += (_, _) => fileList.UpdateRecentFiles(files);
        fileList.OnFileSelected += (file) => OnFileSelected?.Invoke(file);
        fileList.OnFileDelete += (file) => files.Remove(file);
        recentFiles!.Append(fileList);
    }

    public HomeView(Gtk.Window window) : this(GtkHelper.FromLocalizedTemplate("HomeView.ui", GetString), "homeView")
    {
        this.window = window;
    }

}
