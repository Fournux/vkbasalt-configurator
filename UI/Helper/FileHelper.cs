namespace UI.Helper;

public static class FileHelper
{
    public static async Task<Gio.File?> Select(Gtk.Window parent, string title, string acceptLabel = "Accept")
    {
        Gtk.FileDialog dialog = Gtk.FileDialog.New();
        dialog.SetTitle(title);
        dialog.SetAcceptLabel(acceptLabel);
        try
        {
            return await dialog.OpenAsync(parent);
        }
        catch (GLib.GException) // Cancel button clicked
        {
            return null;
        }
    }
}