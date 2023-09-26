namespace UI.Components;

public class KeySelectorRow : Adw.ActionRow
{
    public string Key
    {
        get => keySelector.Key;
        set => keySelector.Key = value;
    }

    private readonly KeySelector keySelector;

    public KeySelectorRow(string title, string pressKeyLabel)
    {
        SetHexpand(true);
        keySelector = new KeySelector(pressKeyLabel);
        AddSuffix(keySelector);
        SetTitle(title);
    }
}
