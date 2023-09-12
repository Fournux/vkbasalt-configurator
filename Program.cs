#pragma warning disable CA1852

global using static Constants.ApplicationIdentity;

Adw.Application app = Adw.Application.New(APP_ID, Gio.ApplicationFlags.DefaultFlags);

app.OnActivate += (application, args) =>
{
    // Create a new MainWindow and show it.
    // The application is passed to the MainWindow so that it can be used
    UI.MainWindow.Window mainWindow = new((Adw.Application)application);

    mainWindow.Show();
};

// Run the application
return app.Run();
