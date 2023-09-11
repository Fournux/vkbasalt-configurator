global using static Constants;

Adw.Application app = Adw.Application.New(APPID, Gio.ApplicationFlags.DefaultFlags);

app.OnActivate += (application, args) =>
{
    // Create a new MainWindow and show it.
    // The application is passed to the MainWindow so that it can be used
    ui.MainWindow mainWindow = new((Adw.Application)application);

    mainWindow.Show();
};

// Run the application
return app.Run();
