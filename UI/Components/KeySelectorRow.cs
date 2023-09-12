namespace UI.Components;

public class KeySelectorRow : Adw.ActionRow
{
    public string Key
    {
        get => keySelector.Key;
        set => keySelector.Key = value;
    }

    private readonly KeySelector keySelector;

    public KeySelectorRow(string title)
    {
        SetHexpand(true);
        keySelector = new KeySelector();
        AddSuffix(keySelector);
        SetTitle(title);
    }
}
