namespace ui;

public class KeySelectorRow : Adw.ActionRow
{
    public string Key
    {
        get { return this._keySelector.Key; }
        set
        {
            this._keySelector.Key = value;
        }
    }

    private readonly KeySelector _keySelector;

    public KeySelectorRow()
    {
        this.SetHexpand(true);
        this._keySelector = new KeySelector();
        this.AddSuffix(this._keySelector);
    }
}
