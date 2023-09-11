using Adw;

namespace ui;

public class CasSettings : Adw.ExpanderRow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Switch? gtkSwitch;
    [Gtk.Connect] private readonly Gtk.SpinButton? gtkSpinButton;
#pragma warning restore 0649

    public bool Enabled
    {
        get { return gtkSwitch!.GetActive(); }
        set { this.gtkSwitch!.SetActive(value); }
    }

    public double Sharpness
    {
        get { return gtkSpinButton!.GetValue(); }
        set { gtkSpinButton!.SetValue(value); }
    }

    private CasSettings(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        gtkSwitch!.BindProperty("active", this, "expanded", GObject.BindingFlags.SyncCreate);
        gtkSwitch!.BindProperty("active", this, "enable-expansion", GObject.BindingFlags.SyncCreate);
    }

    public CasSettings() : this(new Gtk.Builder("CasSettings.ui"), "casSettings")
    {

    }
}
