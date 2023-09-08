public static class FileHelper
{
    public static async Task<Gio.File?> Select(Gtk.Window parent, string title, string acceptLabel = "Accept")
    {
        var dialog = Gtk.FileDialog.New();
        dialog.SetTitle(title);
        dialog.SetAcceptLabel(acceptLabel);
        return await dialog.OpenAsync(parent);
    }
}