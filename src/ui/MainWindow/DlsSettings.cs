using Adw;

namespace ui;

public class DlsSettings : Adw.ExpanderRow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Switch? gtkSwitch;
    [Gtk.Connect] private readonly Gtk.SpinButton? spinSharpening;
    [Gtk.Connect] private readonly Gtk.SpinButton? spinDenoise;
#pragma warning restore 0649

    public bool Active
    {
        get { return this.gtkSwitch!.GetActive(); }
        set { this.gtkSwitch!.SetActive(value); }
    }

    public double Sharpening
    {
        get { return this.spinSharpening!.GetValue(); }
        set { this.spinSharpening!.SetValue(value); }
    }

    public double Denoise
    {
        get { return this.spinDenoise!.GetValue(); }
        set { this.spinDenoise!.SetValue(value); }
    }

    private DlsSettings(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        this.gtkSwitch!.BindProperty("active", this, "expanded", GObject.BindingFlags.SyncCreate);
        this.gtkSwitch!.BindProperty("active", this, "enable-expansion", GObject.BindingFlags.SyncCreate);
    }

    public DlsSettings() : this(new Gtk.Builder("DlsSettings.ui"), "dlsSettings")
    {
    }
}
