namespace ui;

public class KeySelectorRow : Adw.ActionRow
{
    public string Key
    {
        get { return this.keySelector.Key; }
        set
        {
            this.keySelector.Key = value;
        }
    }

    private readonly KeySelector keySelector;

    public KeySelectorRow(string title)
    {
        this.SetHexpand(true);
        this.keySelector = new KeySelector();
        this.AddSuffix(this.keySelector);
        this.SetTitle(title);
    }
}
