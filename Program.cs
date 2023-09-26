#pragma warning disable CA1852

global using Constants;

Adw.Application app = Adw.Application.New(Values.Instance().APP_ID, Gio.ApplicationFlags.DefaultFlags);

app.OnActivate += (application, args) =>
{
    Gtk.IconTheme.GetForDisplay(Gdk.Display.GetDefault()!).AddSearchPath("icons");

    UI.Windows.Main.MainWindow mainWindow = new((Adw.Application)application);
    mainWindow.SetIconName(Values.Instance().APP_ID);
    mainWindow.Show();
};

// Run the application
return app.Run();
