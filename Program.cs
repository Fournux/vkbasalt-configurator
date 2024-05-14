#pragma warning disable CA1852

Adw.Application app = Adw.Application.New(Configuration.Get("APP_ID"), Gio.ApplicationFlags.DefaultFlags);

app.OnActivate += (application, args) =>
{
    Gtk.IconTheme.GetForDisplay(Gdk.Display.GetDefault()!).AddSearchPath("icons");

    UI.Windows.Main.MainWindow mainWindow = new((Adw.Application)application);
    mainWindow.SetIconName(Configuration.Get("APP_ID"));
    mainWindow.Show();
};

// Run the application
return app.Run(0, null);
