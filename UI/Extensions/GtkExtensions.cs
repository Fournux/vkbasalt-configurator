namespace UI.Extensions;

public static class GtkExtensions
{
    public static void Append(this Gtk.Box self, params Gtk.Widget[] widgets)
    {
        foreach (Gtk.Widget widget in widgets)
        {
            self.Append(widget);
        }
    }

    public static void Append(this Gtk.ListBox self, params Gtk.Widget[] widgets)
    {
        foreach (Gtk.Widget widget in widgets)
        {
            self.Append(widget);
        }
    }
}