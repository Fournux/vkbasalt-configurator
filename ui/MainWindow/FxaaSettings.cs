using Adw;

namespace ui;

public class FxaaSettings : Adw.ExpanderRow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Switch? gtk_switch;
    [Gtk.Connect] private readonly Gtk.SpinButton? spin_subpixel;
    [Gtk.Connect] private readonly Gtk.SpinButton? spin_edge;
    [Gtk.Connect] private readonly Gtk.SpinButton? spin_edge_min;
#pragma warning restore 0649

    public bool Active
    {
        get { return this.gtk_switch!.GetActive(); }
        set { this.gtk_switch!.SetActive(value); }
    }

    public double Subpixel
    {
        get { return this.spin_subpixel!.GetValue(); }
        set { this.spin_subpixel!.SetValue(value); }
    }

    public double Edge
    {
        get { return this.spin_edge!.GetValue(); }
        set { this.spin_edge!.SetValue(value); }
    }

    public double EdgeMin
    {
        get { return this.spin_edge_min!.GetValue(); }
        set { this.spin_edge_min!.SetValue(value); }
    }

    private FxaaSettings(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        this.gtk_switch!.BindProperty("active", this, "expanded", GObject.BindingFlags.SyncCreate);
        this.gtk_switch!.BindProperty("active", this, "enable-expansion", GObject.BindingFlags.SyncCreate);
    }

    public FxaaSettings(bool active, double subpixel, double edge, double edgeMin) : this(new Gtk.Builder("FxaaSettings.ui"), "fxaa_settings")
    {
        this.Active = active;
        this.Subpixel = subpixel;
        this.Edge = edge;
        this.EdgeMin = edgeMin;
    }
}
