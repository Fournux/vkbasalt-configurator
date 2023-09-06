using Adw;

namespace ui;

public class CasSettings : Adw.ExpanderRow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Switch? gtk_switch;
    [Gtk.Connect] private readonly Gtk.SpinButton? gtk_spin_button;
#pragma warning restore 0649


    public bool Active
    {
        get { return this.gtk_switch!.GetActive(); }
        set { this.gtk_switch!.SetActive(value); }
    }

    public double Sharpening
    {
        get { return this.gtk_spin_button!.GetValue(); }
        set { this.gtk_spin_button!.SetValue(value); }
    }

    private CasSettings(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        this.gtk_switch!.BindProperty("active", this, "expanded", GObject.BindingFlags.SyncCreate);
        this.gtk_switch!.BindProperty("active", this, "enable-expansion", GObject.BindingFlags.SyncCreate);
    }

    public CasSettings(bool active, double sharpening) : this(new Gtk.Builder("CasSettings.ui"), "cas_settings")
    {
        this.Active = active;
        this.Sharpening = sharpening;
    }
}
