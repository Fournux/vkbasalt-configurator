// This will make all the constants available in the global namespace, 
// so you can use them without the Constants prefix.
global using static Constants;

var app = Adw.Application.New(APP_ID, Gio.ApplicationFlags.DefaultFlags);

app.OnActivate += (application, args) => {
    // Create a new MainWindow and show it.
    // The application is passed to the MainWindow so that it can be used
    var mainWindow = new ui.MainWindow((Adw.Application) application);

    mainWindow.Show();
};

// Run the application
return app.Run();
