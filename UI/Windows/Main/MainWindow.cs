using Core;
using Core.ApplicationState;
using UI.Helpers;
using UI.Windows.About;
using UI.Windows.Main.Config;
using UI.Windows.Main.Home;
using static UI.Localization.CatalogManager;

namespace UI.Windows.Main;

public class MainWindow : Gtk.ApplicationWindow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Adw.Clamp? main;
    [Gtk.Connect] private readonly Adw.SplitButton? openMenuButton;
    [Gtk.Connect] private readonly Gtk.Box? topActions;
    [Gtk.Connect] private readonly Gtk.Button? addButton;
    [Gtk.Connect] private readonly Gtk.Button? saveButton;
    [Gtk.Connect] private readonly Gtk.Button? aboutButton;
    [Gtk.Connect] private readonly Adw.ToastOverlay? toastOverlay;
#pragma warning restore 0649

    private readonly HomeView homeView;
    private readonly ConfigView configView;
    private readonly OpenConfigPopover openConfigPopover;
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

        openConfigPopover = new OpenConfigPopover();
        openConfigPopover.OnFileSelected += file => { OpenConfigFile(file); openConfigPopover.Hide(); };
        openMenuButton!.OnClicked += async (sender, args) => await SelectConfigFile();
        openMenuButton!.SetPopover(openConfigPopover);

        aboutButton!.OnClicked += (sender, args) => AboutWindow.Show(this);
    }

    private async Task CreateConfigFile()
    {
        Gio.File? file = await GtkHelper.SelectFolder(this, GetString("Select a folder"), GetString("Open"));
        if (file is not null)
        {
            OpenConfigFile(file.GetPath()! + Path.DirectorySeparatorChar + "vkbasalt.conf");
        }
    }

    private async Task SelectConfigFile()
    {
        Gio.File? file = await GtkHelper.Select(this, GetString("Select a config file"), GetString("Open"), "*.conf");
        if (file is not null)
        {
            OpenConfigFile(file.GetPath()!);
        }
    }

    private void OpenConfigFile(string file)
    {
        configFile = new ConfigFile(file);
        configView!.LoadConfigFile(configFile);
        _ = StateManager.State.RecentFiles.Add(file);
        SetTitle(file);
        main!.SetChild(configView);
        topActions!.SetVisible(true);
        saveButton!.SetVisible(true);
    }

    private void SaveConfigFile()
    {
        configView!.UpdateConfigFile(configFile!);
        configFile!.Save();
        ShowToast(GetString("Config file has been saved."));
    }

    public MainWindow(Adw.Application application) : this(GtkHelper.FromLocalizedTemplate("MainWindow.ui", GetString), "mainWindow")
    {
        Application = application;
    }

    public void ShowToast(string message)
    {
        toastOverlay!.AddToast(Adw.Toast.New(message));
    }
}
