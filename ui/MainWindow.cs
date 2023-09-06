namespace ui;

public class MainWindow : Gtk.ApplicationWindow
{

    [Gtk.Connect] private readonly Gtk.Box main_box;

    private readonly KeySelectorRow _keySelectorRow;
    private readonly SwitchRow _switchRow;

    private MainWindow(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        _keySelectorRow = new KeySelectorRow() { Key = "Home" };
        _keySelectorRow.SetTitle("Toggle key");
        _switchRow = new SwitchRow() { Active = true };
        _switchRow.SetTitle("Enable on start");
        main_box!.Append(_switchRow);
        main_box.Append(_keySelectorRow);

        // Do any initialization, or connect signals here.
    }


    public MainWindow(Adw.Application application) : this(new Gtk.Builder("MainWindow.ui"), "main_window")
    {
        this.Application = application;
    }
}
