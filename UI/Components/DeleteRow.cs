namespace UI.Components;

public class DeleteRow : Adw.ActionRow
{
#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Button? deleteButton;
#pragma warning restore 0649
    public delegate void OnDeleteHandler();
    public event OnDeleteHandler? OnDelete;


    private DeleteRow(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);
        deleteButton!.OnClicked += (sender, args) =>
        {
            OnDelete?.Invoke();
        };
    }

    public DeleteRow(string title) : this(new Gtk.Builder("DeleteRow.ui"), "deleteRow")
    {
        this.SetTitle(title);
    }
}