namespace ui;

public class MainWindow : Gtk.ApplicationWindow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Box main_box;

#pragma warning restore 0649

    private readonly KeySelectorRow keySelectorRow;
    private readonly SwitchRow switchRow;
    private readonly CasSettings casSettings;
    private readonly DlsSettings dlsSettings;
    private readonly FxaaSettings fxaaSettings;
    private readonly SmaaSettings smaaSettings;

    private MainWindow(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        this.keySelectorRow = new KeySelectorRow() { Key = "Home" };
        this.keySelectorRow.SetTitle("Toggle key");
        this.switchRow = new SwitchRow() { Active = true };
        this.switchRow.SetTitle("Enable on start");
        this.casSettings = new CasSettings { Active = true, Sharpening = 0.5 };
        this.dlsSettings = new DlsSettings { Active = true, Sharpening = 0.8, Denoise = 0.2 };
        this.fxaaSettings = new FxaaSettings { Active = true, Edge = 0.3, EdgeMin = 0.8 };
        this.smaaSettings = new SmaaSettings { Active = true, Edge = 0.8, Steps = 6, DiagSteps = 9 };

        main_box!.Append(switchRow);
        main_box!.Append(keySelectorRow);
        main_box!.Append(casSettings);
        main_box!.Append(dlsSettings);
        main_box!.Append(fxaaSettings);
        main_box!.Append(smaaSettings);
    }

    public MainWindow(Adw.Application application) : this(new Gtk.Builder("MainWindow.ui"), "main_window")
    {
        this.Application = application;
    }
}
