namespace UI.Helpers;

public static class GtkHelper
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

    public static async Task<Gio.File?> SelectFolder(Gtk.Window parent, string title, string acceptLabel = "Accept")
    {
        Gtk.FileDialog dialog = Gtk.FileDialog.New();
        dialog.SetTitle(title);
        dialog.SetAcceptLabel(acceptLabel);
        try
        {
            return await dialog.SelectFolderAsync(parent);
        }
        catch (GLib.GException) // Cancel button clicked
        {
            return null;
        }
    }
}