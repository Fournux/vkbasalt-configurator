using Core;
using Core.ApplicationState;
using UI.Helper;
using UI.Window.Main.Config;
using UI.Window.Main.Home;

namespace UI.Window.Main;

public class MainWindow : Gtk.ApplicationWindow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Adw.Clamp? main;
    [Gtk.Connect] private readonly Gtk.MenuButton? openMenuButton;
    [Gtk.Connect] private readonly Gtk.Button? addButton;
    [Gtk.Connect] private readonly Gtk.Button? saveButton;
    [Gtk.Connect] private readonly Adw.ToastOverlay? toast;
#pragma warning restore 0649

    private readonly HomeView? homeView;
    private readonly ConfigView? configView;

    private ConfigFile? configFile;

    private MainWindow(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        configView = new ConfigView();

        homeView = new HomeView(this);
        homeView.OnFileSelected += OpenConfigFile;
        homeView.OnCreateConfigFile += async (_, _) => await CreateConfigFile();
        homeView.OnSelectConfigFile += async (_, _) => await SelectConfigFile();
        main!.SetChild(homeView);

        addButton!.OnClicked += async (_, _) => await CreateConfigFile();
        saveButton!.OnClicked += (_, _) => SaveConfigFile();

        OnCloseRequest += (_, _) =>
        {
            StateManager.Persist();
            return false;
        };

        openMenuButton!.SetPopover(new OpenConfigPopover());
    }

    private async Task CreateConfigFile()
    {
        Gio.File? file = await GtkHelper.SelectFolder(this, "Select a folder", "Open");
        if (file != null)
        {
            OpenConfigFile(file.GetPath()! + Path.DirectorySeparatorChar + "vkbasalt.conf");
        }
    }

    private async Task SelectConfigFile()
    {
        Gio.File? file = await GtkHelper.Select(this, "Select a config file", "Open");
        if (file != null)
        {
            OpenConfigFile(file.GetPath()!);
        }
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

    public MainWindow(Adw.Application application) : this(new Gtk.Builder("MainWindow.ui"), "mainWindow")
    {
        Application = application;
    }

    public void ShowToast(string message)
    {
        toast!.AddToast(Adw.Toast.New(message));
    }
}
