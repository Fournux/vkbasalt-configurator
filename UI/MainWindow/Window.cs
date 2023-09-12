using Core;
using Core.ApplicationState;

namespace UI.MainWindow;

public class Window : Gtk.ApplicationWindow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Adw.Clamp? clamp;
    [Gtk.Connect] private readonly Gtk.Button? saveButton;
    [Gtk.Connect] private readonly Gtk.Button? closeButton;
#pragma warning restore 0649

    private readonly HomeView.View? homeView;
    private readonly ConfigView.View? configView;

    private ConfigFile? configFile;

    private Window(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        configView = new ConfigView.View();

        homeView = new HomeView.View(this);
        homeView.OnFileSelected += OpenConfigFile;
        clamp!.SetChild(homeView);

        closeButton!.OnClicked += (_, _) => CloseConfigFile();
        saveButton!.OnClicked += (_, _) => SaveConfigFile();
        OnCloseRequest += (_, _) =>
        {
            StateManager.Persist();
            return false;
        };
    }

    private void OpenConfigFile(string file)
    {
        configFile = new ConfigFile(file);
        configView!.LoadConfigFile(configFile);
        _ = StateManager.State.RecentFiles.Add(file);

        clamp!.SetChild(configView);
        closeButton!.SetVisible(true);
        saveButton!.SetVisible(true);
    }

    private void CloseConfigFile()
    {
        SaveConfigFile();
        clamp!.SetChild(homeView);
        closeButton!.SetVisible(false);
        saveButton!.SetVisible(false);
    }

    private void SaveConfigFile()
    {
        configView!.UpdateConfigFile(configFile!);
        configFile!.Save();
    }

    public Window(Adw.Application application) : this(new Gtk.Builder("MainWindow.ui"), "mainWindow")
    {
        Application = application;
    }
}
