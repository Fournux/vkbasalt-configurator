using Core;
using Core.ApplicationState;

namespace UI.MainWindow;

public class Window : Gtk.ApplicationWindow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Adw.Clamp? main;
    [Gtk.Connect] private readonly Gtk.Button? saveButton;
    [Gtk.Connect] private readonly Adw.ToastOverlay? toast;
    [Gtk.Connect] private readonly Adw.SplitButton splitButton;
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
        main!.SetChild(homeView);

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
        main!.SetChild(configView);
        saveButton!.SetVisible(true);
    }

    private void SaveConfigFile()
    {
        configView!.UpdateConfigFile(configFile!);
        configFile!.Save();
        ShowToast("Config file has been saved.");
    }

    public Window(Adw.Application application) : this(new Gtk.Builder("MainWindow.ui"), "mainWindow")
    {
        Application = application;

    }

    public void ShowToast(string message)
    {
        toast!.AddToast(Adw.Toast.New(message));
    }
}
