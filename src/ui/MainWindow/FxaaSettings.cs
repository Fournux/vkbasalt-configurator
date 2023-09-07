using Adw;

namespace ui;

public class FxaaSettings : Adw.ExpanderRow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Switch? gtkSwitch;
    [Gtk.Connect] private readonly Gtk.SpinButton? spinSubpixel;
    [Gtk.Connect] private readonly Gtk.SpinButton? spinEdge;
    [Gtk.Connect] private readonly Gtk.SpinButton? spinEdgeMin;
#pragma warning restore 0649

    public bool Active
    {
        get { return this.gtkSwitch!.GetActive(); }
        set { this.gtkSwitch!.SetActive(value); }
    }

    public double Subpixel
    {
        get { return this.spinSubpixel!.GetValue(); }
        set { this.spinSubpixel!.SetValue(value); }
    }

    public double Edge
    {
        get { return this.spinEdge!.GetValue(); }
        set { this.spinEdge!.SetValue(value); }
    }

    public double EdgeMin
    {
        get { return this.spinEdgeMin!.GetValue(); }
        set { this.spinEdgeMin!.SetValue(value); }
    }

    private FxaaSettings(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        this.gtkSwitch!.BindProperty("active", this, "expanded", GObject.BindingFlags.SyncCreate);
        this.gtkSwitch!.BindProperty("active", this, "enable-expansion", GObject.BindingFlags.SyncCreate);
    }

    public FxaaSettings() : this(new Gtk.Builder("FxaaSettings.ui"), "fxaaSettings")
    {
    }
}
