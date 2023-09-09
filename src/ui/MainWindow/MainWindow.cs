using GLib;

namespace ui;

public class MainWindow : Gtk.ApplicationWindow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Adw.Clamp? clamp;
    [Gtk.Connect] private readonly Gtk.Button? saveButton;
    [Gtk.Connect] private readonly Gtk.Button? closeButton;

#pragma warning restore 0649

    private readonly HomeView? homeView;
    private readonly ConfigView? configView;

    private ConfigFile? configFile;

    private MainWindow(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        configView = new ConfigView();

        homeView = new HomeView(this);
        homeView.OnFileSelected += this.OpenConfigFile;
        clamp!.SetChild(homeView);

        closeButton!.OnClicked += (sender, args) =>
        {
            CloseConfigFile();
        };

    }

    private void OpenConfigFile(Gio.File file)
    {
        clamp!.SetChild(configView);
        configFile = new ConfigFile(file.GetPath()!);
        configView!.LoadConfigFile(configFile);
        closeButton!.SetVisible(true);
        saveButton!.SetVisible(true);
    }

    private void CloseConfigFile()
    {

        clamp!.SetChild(homeView);
        closeButton!.SetVisible(false);
        saveButton!.SetVisible(false);
    }

    public MainWindow(Adw.Application application) : this(new Gtk.Builder("MainWindow.ui"), "mainWindow")
    {
        this.Application = application;
    }
}
