#pragma warning disable CA1852

global using static Constants.ApplicationIdentity;

Adw.Application app = Adw.Application.New(APP_ID, Gio.ApplicationFlags.DefaultFlags);

app.OnActivate += (application, args) =>
{
    Gtk.IconTheme.GetForDisplay(Gdk.Display.GetDefault()!).AddSearchPath("icons");

    UI.Windows.Main.MainWindow mainWindow = new((Adw.Application)application);
    mainWindow.SetIconName(APP_ID);
    mainWindow.Show();
};

// Run the application
return app.Run();
