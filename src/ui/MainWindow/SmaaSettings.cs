using Adw;

namespace ui;

public class SmaaSettings : Adw.ExpanderRow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Switch? gtkSwitch;
    [Gtk.Connect] private readonly Gtk.ToggleButton? toggleLuma;
    [Gtk.Connect] private readonly Gtk.ToggleButton? toggleColor;
    [Gtk.Connect] private readonly Gtk.SpinButton? spinEdge;
    [Gtk.Connect] private readonly Gtk.SpinButton? spinSteps;
    [Gtk.Connect] private readonly Gtk.SpinButton? spinDiagSteps;
    [Gtk.Connect] private readonly Gtk.SpinButton? spinCorner;

#pragma warning restore 0649

    public bool Enabled
    {
        get { return gtkSwitch!.GetActive(); }
        set { gtkSwitch!.SetActive(value); }
    }
    public double Edge
    {
        get { return spinEdge!.GetValue(); }
        set { spinEdge!.SetValue(value); }
    }

    public double Steps
    {
        get { return spinSteps!.GetValue(); }
        set { spinSteps!.SetValue(value); }
    }
    public double DiagSteps
    {
        get { return spinDiagSteps!.GetValue(); }
        set { spinDiagSteps!.SetValue(value); }
    }
    public double Corner
    {
        get { return spinCorner!.GetValue(); }
        set { spinCorner!.SetValue(value); }
    }

    public SmaaEdgeDetection EdgeDetection
    {
        get { return toggleColor!.Active ? SmaaEdgeDetection.Color : SmaaEdgeDetection.Luma; }
        set
        {
            var toggle = value == SmaaEdgeDetection.Luma ? toggleLuma : toggleColor;
            if (!toggle!.Active)
            {
                toggle.SetActive(true);
            }
        }
    }

    private SmaaSettings(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        this.gtkSwitch!.BindProperty("active", this, "expanded", GObject.BindingFlags.SyncCreate);
        this.gtkSwitch!.BindProperty("active", this, "enable-expansion", GObject.BindingFlags.SyncCreate);
    }

    public SmaaSettings() : this(new Gtk.Builder("SmaaSettings.ui"), "smaaSettings")
    {
        this.EdgeDetection = SmaaEdgeDetection.Color;
    }
}
