namespace ui;

public class SwitchRow : Adw.ActionRow
{
    public bool Active
    {
        get { return this._switch.GetActive(); }
        set
        {
            this._switch.SetActive(value);
        }
    }

    private readonly Gtk.Switch _switch;

    public SwitchRow()
    {
        this.SetHexpand(true);
        this._switch = new Gtk.Switch();
        this._switch.SetValign(Gtk.Align.Center);
        this.AddSuffix(this._switch);
    }
}
