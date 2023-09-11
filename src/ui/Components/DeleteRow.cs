namespace ui;

public class DeleteRow : Adw.ActionRow
{
    private readonly Gtk.Button deleteButton;
    public delegate void OnDeleteHandler();
    public event OnDeleteHandler? OnDelete;
    public DeleteRow(string title)
    {
        var content = new Adw.ButtonContent();
        content.SetIconName("close-symbolic");

        deleteButton = new Gtk.Button();
        deleteButton.AddCssClass("circular");
        deleteButton.AddCssClass("flat");
        deleteButton.SetValign(Gtk.Align.Center);
        deleteButton.SetChild(content);

        deleteButton!.OnClicked += (sender, args) =>
        {
            OnDelete?.Invoke();
        };

        AddSuffix(deleteButton);
        SetTitle(title);
    }
}
