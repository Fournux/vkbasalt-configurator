namespace ui;

public class KeySelector : Gtk.Button
{
    private readonly Gtk.EventControllerKey keyController;
    public string Key
    {
        get { return this.GetLabel()!; }
        set
        {
            this.SetLabel(value);
        }
    }
    private bool Listening { get; set; } = false;

    public KeySelector() : base()
    {
        this.SetValign(Gtk.Align.Center);

        keyController = Gtk.EventControllerKey.New();
        keyController.OnKeyPressed += (_, args) =>
        {
            if (this.Listening)
            {
                this.Key = Gdk.Functions.KeyvalName(args.Keyval)!;

                this.Listening = false;
                return false;
            }
            else
            {
                return true;
            }

        };

        this.AddController(keyController);


        this.OnClicked += (_, args) =>
        {
            this.SetLabel("Press any key");
            this.Listening = true;
        };

        this.OnNotify += (_, args) =>
        {
            if (args.Pspec.GetName() == "has_focus")
            {
                if (!this.HasFocus)
                {
                    this.SetLabel(this.Key);
                }
            }
        };
    }

}
