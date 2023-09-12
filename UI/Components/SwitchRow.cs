namespace UI.Components;

public class SwitchRow : Adw.ActionRow
{
    public bool Active
    {
        get => gtkSwitch.GetActive();
        set => gtkSwitch.SetActive(value);
    }

    private readonly Gtk.Switch gtkSwitch;

    public SwitchRow(string title)
    {
        SetHexpand(true);
        gtkSwitch = new Gtk.Switch();
        gtkSwitch.SetValign(Gtk.Align.Center);
        AddSuffix(gtkSwitch);
        SetTitle(title);
    }
}
