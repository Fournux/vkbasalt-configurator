using UI.Components;

namespace UI.Window.Main;

public class FileList : Gtk.ListBox
{
    public delegate void FileHandler(string file);
    public event FileHandler? OnFileSelected;
    public event FileHandler? OnFileDelete;

    public FileList(ICollection<string> files)
    {
        SetActivateOnSingleClick(true);
        SetHexpand(true);
        AddCssClass("boxed-list");

        OnRowActivated += (sender, args) =>
        {
            DeleteRow row = (DeleteRow)args.Row;
            OnFileSelected?.Invoke(row.GetTitle());
        };

        UpdateRecentFiles(files);
    }

    public void UpdateRecentFiles(ICollection<string> files)
    {
        for (Gtk.Widget? child; (child = GetLastChild()) != null;)
        {
            Remove(child);
        }

        if (files.Count == 0)
        {
            SetVisible(false);
        }
        else
        {
            SetVisible(true);
        }

        foreach (string file in files)
        {
            DeleteRow row = new(file);
            row.OnDelete += () => OnFileDelete?.Invoke(file);
            row.SetActivatable(true);
            Prepend(row);
        }
    }
}
