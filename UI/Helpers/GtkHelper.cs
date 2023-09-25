using System.Reflection;
using System.Xml;

namespace UI.Helpers;

public static class GtkHelper
{
    public static async Task<Gio.File?> Select(Gtk.Window parent, string title, string acceptLabel = "Accept", string? pattern = null)
    {
        Gtk.FileDialog dialog = Gtk.FileDialog.New();
        dialog.SetTitle(title);
        dialog.SetAcceptLabel(acceptLabel);

        if (pattern is not null)
        {
            Gtk.FileFilter filter = Gtk.FileFilter.New();
            filter.AddPattern(pattern);
            dialog.SetDefaultFilter(filter);
        }

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

    public static Gtk.Builder FromLocalizedTemplate(string template, Func<string, string> getString)
    {
        using Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(template) ?? throw new FileNotFoundException("Cannot get resource file '" + template + "'");
        using StreamReader reader = new(stream);
        XmlDocument xml = new();
        xml.LoadXml(reader.ReadToEnd());

        foreach (XmlElement element in xml.SelectNodes("//*[@translatable]")!)
        {
            element.RemoveAttribute("translatable");
            element.InnerText = getString(element.InnerText);
        }

        return Gtk.Builder.NewFromString(xml.OuterXml, -1);
    }
}