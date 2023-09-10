using core.ApplicationState;
using GLib;
using core;

namespace ui;

public class MainWindow : Gtk.ApplicationWindow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Adw.Clamp? clamp;
    [Gtk.Connect] private readonly Gtk.Button? saveButton;
    [Gtk.Connect] private readonly Gtk.Button? closeButton;
    [Gtk.Connect] private readonly Gtk.Label? subtitle;


#pragma warning restore 0649

    private readonly HomeView? homeView;
    private readonly ConfigView? configView;

    private ConfigFile? configFile;

    private MainWindow(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        this.configView = new ConfigView();

        this.homeView = new HomeView(this);
        this.homeView.OnFileSelected += this.OpenConfigFile;
        this.clamp!.SetChild(homeView);

        this.closeButton!.OnClicked += (_, _) => this.CloseConfigFile();
        this.saveButton!.OnClicked += (_, _) => this.SaveConfigFile();
        this.OnCloseRequest += (_, _) =>
        {
            StateManager.Persist();
            return false;
        };
    }

    private void OpenConfigFile(string file)
    {
        configFile = new ConfigFile(file);
        configView!.LoadConfigFile(configFile);
        StateManager.State.RecentFiles.Add(file);


        clamp!.SetChild(configView);
        closeButton!.SetVisible(true);
        saveButton!.SetVisible(true);
    }

    private void CloseConfigFile()
    {
        this.SaveConfigFile();
        clamp!.SetChild(homeView);
        closeButton!.SetVisible(false);
        saveButton!.SetVisible(false);
    }

    private void SaveConfigFile()
    {
        configView!.UpdateConfigFile(configFile!);
        configFile!.Save();
    }

    public MainWindow(Adw.Application application) : this(new Gtk.Builder("MainWindow.ui"), "mainWindow")
    {
        this.Application = application;
    }
}
