using GLib;
using ui.Helper;

namespace ui;

public class HomeView : Gtk.Box
{
    private Gtk.Window? window;

#pragma warning disable 0649
    [Gtk.Connect] private readonly Gtk.Button? buttonExisting;
    [Gtk.Connect] private readonly Gtk.Button? buttonNew;
#pragma warning restore 0649

    public delegate void FileSelectedCallback(Gio.File file);
    public event FileSelectedCallback? OnFileSelected;

    private HomeView(Gtk.Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);
        buttonExisting!.OnClicked += async (sender, args) =>
        {
            Gio.File? file = await FileHelper.Select(window!, "Select a file", "Open");
            if (file != null)
            {
                OnFileSelected?.Invoke(file);
            }
        };
    }

    public HomeView(Gtk.Window window) : this(new Gtk.Builder("HomeView.ui"), "homeView")
    {
        this.window = window;
    }
}
