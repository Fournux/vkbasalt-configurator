namespace UI.Windows.Main.Config;

public class CasSettings : Adw.ExpanderRow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Switch? gtkSwitch;
    [Gtk.Connect] private readonly Gtk.SpinButton? gtkSpinButton;
#pragma warning restore 0649

    public bool Enabled
    {
        get => gtkSwitch!.GetActive();
        set => gtkSwitch!.SetActive(value);
    }

    public double Sharpness
    {
        get => gtkSpinButton!.GetValue();
        set => gtkSpinButton!.SetValue(value);
    }

    private CasSettings(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        _ = gtkSwitch!.BindProperty("active", this, "expanded", GObject.BindingFlags.SyncCreate);
        _ = gtkSwitch!.BindProperty("active", this, "enable-expansion", GObject.BindingFlags.SyncCreate);
    }

    public CasSettings() : this(new Gtk.Builder("CasSettings.ui"), "casSettings")
    {

    }
}
