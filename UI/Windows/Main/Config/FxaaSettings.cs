
using UI.Helpers;
using static UI.Localization.CatalogManager;

namespace UI.Windows.Main.Config;

public class FxaaSettings : Adw.ExpanderRow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Switch? gtkSwitch;
    [Gtk.Connect] private readonly Gtk.SpinButton? spinSubpixel;
    [Gtk.Connect] private readonly Gtk.SpinButton? spinEdge;
    [Gtk.Connect] private readonly Gtk.SpinButton? spinEdgeMin;
#pragma warning restore 0649

    public bool Enabled
    {
        get => gtkSwitch!.GetActive();
        set => gtkSwitch!.SetActive(value);
    }

    public double Subpixel
    {
        get => spinSubpixel!.GetValue();
        set => spinSubpixel!.SetValue(value);
    }

    public double Edge
    {
        get => spinEdge!.GetValue();
        set => spinEdge!.SetValue(value);
    }

    public double EdgeMin
    {
        get => spinEdgeMin!.GetValue();
        set => spinEdgeMin!.SetValue(value);
    }

    private FxaaSettings(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        _ = gtkSwitch!.BindProperty("active", this, "expanded", GObject.BindingFlags.SyncCreate);
        _ = gtkSwitch!.BindProperty("active", this, "enable-expansion", GObject.BindingFlags.SyncCreate);
    }

    public FxaaSettings() : this(GtkHelper.FromLocalizedTemplate("FxaaSettings.ui", GetString), "fxaaSettings")
    {
    }
}
