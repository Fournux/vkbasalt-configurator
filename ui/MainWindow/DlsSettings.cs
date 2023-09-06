using Adw;

namespace ui;

public class DlsSettings : Adw.ExpanderRow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Switch? gtk_switch;
    [Gtk.Connect] private readonly Gtk.SpinButton? spin_sharpening;
    [Gtk.Connect] private readonly Gtk.SpinButton? spin_denoise;
#pragma warning restore 0649

    public bool Active
    {
        get { return this.gtk_switch!.GetActive(); }
        set { this.gtk_switch!.SetActive(value); }
    }

    public double Sharpening
    {
        get { return this.spin_sharpening!.GetValue(); }
        set { this.spin_sharpening!.SetValue(value); }
    }

    public double Denoise
    {
        get { return this.spin_denoise!.GetValue(); }
        set { this.spin_denoise!.SetValue(value); }
    }

    private DlsSettings(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        this.gtk_switch!.BindProperty("active", this, "expanded", GObject.BindingFlags.SyncCreate);
        this.gtk_switch!.BindProperty("active", this, "enable-expansion", GObject.BindingFlags.SyncCreate);
    }

    public DlsSettings(bool active, double sharpening, double denoise) : this(new Gtk.Builder("DlsSettings.ui"), "dls_settings")
    {
        this.Active = active;
        this.Sharpening = sharpening;
        this.Denoise = denoise;
    }
}
