using Core;
using Core.ApplicationState;
using UI.Helpers;
using UI.Windows.About;
using UI.Windows.Main.Config;
using UI.Windows.Main.Home;

namespace UI.Windows.Main;

public class MainWindow : Gtk.ApplicationWindow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Adw.Clamp? main;
    [Gtk.Connect] private readonly Gtk.MenuButton? openMenuButton;
    [Gtk.Connect] private readonly Gtk.Box? topActions;
    [Gtk.Connect] private readonly Gtk.Button? addButton;
    [Gtk.Connect] private readonly Gtk.Button? saveButton;
    [Gtk.Connect] private readonly Gtk.Button? aboutButton;

    [Gtk.Connect] private readonly Adw.ToastOverlay? toast;
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
        openConfigPopover.OnSelectConfigFile += async (sender, args) => { openConfigPopover.Hide(); await SelectConfigFile(); };
        openMenuButton!.SetPopover(openConfigPopover);

        aboutButton!.OnClicked += (sender, args) => AboutWindow.Show(this);
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
        SetTitle(file);
        main!.SetChild(configView);
        topActions!.SetVisible(true);
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


        Gtk.IconTheme test = Gtk.IconTheme.GetForDisplay(Gdk.Display.GetDefault()!);
        test.AddSearchPath("icons");
        Console.WriteLine(test.HasIcon("lu.fournux.vkbasalt.configurator"));

        Console.WriteLine("coucou");
    }

    public void ShowToast(string message)
    {
        toast!.AddToast(Adw.Toast.New(message));
    }
}
