namespace UI.Components;

public class KeySelector : Gtk.Button
{
    private readonly Gtk.EventControllerKey keyController;
    public string Key
    {
        get => GetLabel()!;
        set => SetLabel(value);
    }
    private bool Listening { get; set; }

    public KeySelector() : base()
    {
        SetValign(Gtk.Align.Center);

        keyController = Gtk.EventControllerKey.New();
        keyController.OnKeyPressed += (_, args) =>
        {
            if (Listening)
            {
                Key = Gdk.Functions.KeyvalName(args.Keyval)!;

                Listening = false;
                return false;
            }
            else
            {
                return true;
            }

        };

        AddController(keyController);

        OnClicked += (_, args) =>
        {
            SetLabel("Press any key");
            Listening = true;
        };

        OnNotify += (_, args) =>
        {
            if (args.Pspec.GetName() == "has_focus")
            {
                if (!HasFocus)
                {
                    SetLabel(Key);
                }
            }
        };
    }
}
