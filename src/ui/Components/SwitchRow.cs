namespace ui;

public class SwitchRow : Adw.ActionRow
{
    public bool Active
    {
        get { return this.gtkSwitch.GetActive(); }
        set
        {
            this.gtkSwitch.SetActive(value);
        }
    }

    private readonly Gtk.Switch gtkSwitch;

    public SwitchRow(string title)
    {
        this.SetHexpand(true);
        this.gtkSwitch = new Gtk.Switch();
        this.gtkSwitch.SetValign(Gtk.Align.Center);
        this.AddSuffix(this.gtkSwitch);
        this.SetTitle(title);
    }
}
