using Adw;

namespace ui;

public class DlsSettings : Adw.ExpanderRow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Switch? gtkSwitch;
    [Gtk.Connect] private readonly Gtk.SpinButton? spinSharpness;
    [Gtk.Connect] private readonly Gtk.SpinButton? spinDenoise;
#pragma warning restore 0649

    public bool Enabled
    {
        get => gtkSwitch!.GetActive();
        set => gtkSwitch!.SetActive(value);
    }

    public double Sharpness
    {
        get => spinSharpness!.GetValue();
        set => spinSharpness!.SetValue(value);
    }

    public double Denoise
    {
        get => spinDenoise!.GetValue();
        set => spinDenoise!.SetValue(value);
    }

    private DlsSettings(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        _ = gtkSwitch!.BindProperty("active", this, "expanded", GObject.BindingFlags.SyncCreate);
        _ = gtkSwitch!.BindProperty("active", this, "enable-expansion", GObject.BindingFlags.SyncCreate);
    }

    public DlsSettings() : this(new Gtk.Builder("DlsSettings.ui"), "dlsSettings")
    {
    }
}
