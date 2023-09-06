namespace ui;

public class KeySelector : Gtk.Button
{
    private readonly Gtk.EventControllerKey _keyController;
    private string _key = string.Empty;
    public string Key
    {
        get { return _key; }
        set
        {
            _key = value;
            this.SetLabel(value);
        }
    }
    private bool Listening { get; set; } = false;

    public KeySelector() : base()
    {
        this.SetValign(Gtk.Align.Center);

        _keyController = Gtk.EventControllerKey.New();
        _keyController.OnKeyPressed += (_, args) =>
        {
            if (this.Listening)
            {
                this.Key = args.Keycode.ToString();
                this.Listening = false;
                return false;
            }
            else
            {
                return true;
            }

        };

        this.AddController(_keyController);


        this.OnClicked += (_, args) =>
        {
            this.SetLabel("Press any key");
            this.Listening = true;
        };

        this.OnNotify += (_self, args) =>
        {
            if (args.Pspec.GetName() == "has_focus")
            {
                var self = (KeySelector)_self;
                if (!self.HasFocus)
                {
                    this.SetLabel(this.Key);
                }
            }
        };

        this.SetLabel("coucou");
    }

}
